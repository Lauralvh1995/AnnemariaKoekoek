using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RedlightGreenlight : MonoBehaviourPunCallbacks
{
    public bool currentEquationCorrect = false;
    public bool gameStarted = false;
    private float stateTimer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private EquationController equationController;
    [SerializeField] private TableManager tableManager;
    [SerializeField] private Equation currentEquation;
    [SerializeField] private Button startButton;

    //TODO replace this preset list with randomisation
    private List<Equation> tempEquations = new List<Equation>()
    {
        new Equation(1, Equation.Operator.Keer, 1, 1),
        new Equation(2, Equation.Operator.Keer, 3, 8),
        new Equation(4, Equation.Operator.Keer, 5, 12),
        new Equation(6, Equation.Operator.Keer, 7, 42),
    };

    private int currentEquationIndex = 0;

    public void StartGame()
    {
        photonView.RPC("StartGameRPC", RpcTarget.All);
    }

    public override void OnCreatedRoom()
    {
        startButton.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient || !gameStarted)
        {
            return;
        }
        else
        {
            stateTimer += Time.deltaTime;
            if (stateTimer > 4)
            {
                stateTimer = 0;

                Equation eq = equationController.SetUpNewEquation();

                photonView.RPC("UpdateEquation", RpcTarget.All, eq.Number1, eq._Operator, eq.Number2, eq.Answer);
            }
        }
    }

    [PunRPC]
    private void StartGameRPC()
    {
        gameStarted = true;
        tableManager.EnableTable(1);
    }

    [PunRPC]
    private void UpdateEquation(int number1, Equation.Operator _operator, int number2, int answer)
    {
        currentEquation = new Equation(number1, _operator, number2, answer);
        StartCoroutine(UpdateEquationCorrectAfterDelay(currentEquation));

        string textToSpeak = number1.ToString() + _operator.ToString() + number2.ToString() + " is " + answer;
        StartCoroutine(CallTextToSpeech(textToSpeak));
    }

    private IEnumerator UpdateEquationCorrectAfterDelay(Equation currentEquation)
    {
        yield return new WaitForSeconds(2);

        if (currentEquation.Answer == currentEquation.Equals())
        {
            currentEquationCorrect = true;
        }
        else
        {
            currentEquationCorrect = false;
        }
    }

    private IEnumerator CallTextToSpeech(string spokenText)
    {
        string url = "https://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q=" + spokenText + "&tl=Nl";
        WWW www = new WWW(url);
        yield return www;
        audioSource.clip = www.GetAudioClip(false, true, AudioType.MPEG);
        audioSource.Play();
    }

}
