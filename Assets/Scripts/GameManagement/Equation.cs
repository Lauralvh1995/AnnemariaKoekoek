using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Equation
{
    public int Number1;
    public Operator _Operator;
    public int Number2;
    public int Answer;

    public enum Operator
    {
        Plus,
        Min,
        Keer,
        GedeeldDoor
    }

    public Equation(int number1, Operator op, int number2, int answer)
    {
        this.Number1 = number1;
        _Operator = op;
        this.Number2 = number2;
        this.Answer = answer;
    }

    public int Equals()
    {
        switch (_Operator)
        {
            case Operator.Plus:
                {
                    return Number1 + Number2;
                }
            case Operator.Min:
                {
                    return Number1 - Number2;
                }
            case Operator.Keer:
                {
                    return Number1 * Number2;
                }
            case Operator.GedeeldDoor:
                {
                    if (Number2 == 0)
                    {
                        Debug.LogError("NIET DELEN DOOR 0");
                        return 0;
                    }
                    return Number1 / Number2;
                }
        }
        Debug.LogWarning("Geen antwoord");
        return 0;
    }

    public override string ToString()
    {
        string output = "";
        switch (_Operator)
        {
            case Operator.Plus:
                output = Number1 + " + " + Number2 + " = " + Equals();
                break;
            case Operator.Min:
                output = Number1 + " - " + Number2 + " = " + Equals();
                break;
            case Operator.Keer:
                output = Number1 + " x " + Number2 + " = " + Equals();
                break;
            case Operator.GedeeldDoor:
                if (Number2 == 0)
                {
                    output = "Je kan niet delen door 0";
                }
                else
                    output = Number1 + " / " + Number2 + " = " + Equals();
                break;
        }

        return output;
    }
}
public enum Operator
{
    Plus,
    Minus,
    Multiply,
    Divide
}
