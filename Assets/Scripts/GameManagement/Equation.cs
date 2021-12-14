using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Equation
{
    public int number1;
    public Operator _operator;
    public int number2;
    
    public enum Operator
    {
        Plus,
        Minus,
        Multiply,
        Divide
    }

    public Equation(int number1, Operator op, int number2)
    {
        this.number1 = number1;
        _operator = op;
        this.number2 = number2;
    }

    public float Equals()
    {
        switch (_operator)
        {
            case Operator.Plus:
                {
                    return number1 + number2;
                }
            case Operator.Minus:
                {
                    return number1 - number2;
                }
            case Operator.Multiply:
                {
                    return number1 * number2;
                }
            case Operator.Divide:
                {
                    if (number2 == 0)
                    {
                        Debug.LogError("NIET DELEN DOOR 0");
                        return 0f;
                    }
                    return (float)number1 / (float)number2;
                }
        }
        Debug.LogWarning("Geen antwoord");
        return 0f;
    }

    public override string ToString()
    {
        string antwoord = "";
        switch (_operator)
        {
            case Operator.Plus:
                antwoord = number1 + " + " + number2 + " = " + Equals();
                break;
            case Operator.Minus:
                antwoord = number1 + " - " + number2 + " = " + Equals();
                break;
            case Operator.Multiply:
                antwoord = number1 + " x " + number2 + " = " + Equals();
                break;
            case Operator.Divide:
                if (number2 == 0)
                {
                    antwoord = "Je kan niet delen door 0";
                }
                else
                    antwoord = number1 + " / " + number2 + " = " + Equals();
                break;
        }

        return antwoord;
    }
}