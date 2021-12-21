using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RedlightGreenlight : MonoBehaviourPun
{
    private float stateTimer;
    [SerializeField] private EquationController equationController;
    [SerializeField] private Equation currentEquation;

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

                Equation eq = equationController.SetUpNewEquation();

                photonView.RPC("UpdateEquation", RpcTarget.All, eq.number1, eq._operator, eq.number2);
            }
        }
    }

    [PunRPC]
    private void UpdateEquation(int number1, Operator _operator, int number2)
    {
        currentEquation = new Equation(number1, _operator, number2);

        //TODO say the sum out loud
    }
    
}
