using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RedlightGreenlight : MonoBehaviourPun
{
    private float stateTimer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Equation currentEquation;

    //TODO replace this preset list with randomisation
    private List<Equation> tempEquations = new List<Equation>()
    {
        new Equation(1, Equation.Operator.Plus, 1),
        new Equation(2, Equation.Operator.Min, 3),
        new Equation(4, Equation.Operator.Keer, 5),
        new Equation(6, Equation.Operator.GedeeldDoor, 7),
    };

    private int currentEquationIndex = 0;


    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        else
        {
            stateTimer += Time.deltaTime;
            if (stateTimer > 3)
            {
                stateTimer = 0;

                //This loops through the preset equations. Use a randomiser instead asap
                currentEquationIndex++;
                if (currentEquationIndex >= tempEquations.Count)
                {
                    currentEquationIndex = 0;
                }

                Equation eq = tempEquations[currentEquationIndex];

                photonView.RPC("UpdateEquation", RpcTarget.All, eq.number1, eq._operator, eq.number2);
            }
        }
    }

    [PunRPC]
    private void UpdateEquation(int number1, Equation.Operator _operator, int number2)
    {
        currentEquation = new Equation(number1, _operator, number2);

        string textToSpeak = number1.ToString() + _operator.ToString() + number2.ToString();
        StartCoroutine(CallTextToSpeech(textToSpeak));
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
