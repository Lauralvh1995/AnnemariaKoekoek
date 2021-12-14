using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RedlightGreenlight : MonoBehaviourPun
{
    private float stateTimer;
    [SerializeField ]private Equation currentEquation;

    //TODO replace this preset list with randomisation
    private List<Equation> tempEquations = new List<Equation>()
    {
        new Equation(1, Equation.Operator.Multiply, 1),
        new Equation(2, Equation.Operator.Multiply, 3),
        new Equation(4, Equation.Operator.Multiply, 5),
        new Equation(6, Equation.Operator.Multiply, 7),
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

        //TODO say the sum out loud
    }
    
}
