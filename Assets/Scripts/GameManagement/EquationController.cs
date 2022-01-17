using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquationController : MonoBehaviour
{
    [SerializeField] private List<Table> possibleTables;
    [SerializeField] private Table currentTable;

    [SerializeField] private Equation currentEquation;
    [SerializeField] private float correctAnswer;
    [SerializeField] private float currentCheckAnswer;

    [SerializeField] private List<float> possibleAnswers;

    public void AddTableToList(Table table)
    {
        possibleTables.Add(table);
        ChangeTable(possibleTables[0]);
        SetUpNewEquation();
    }
    
    public void ClearTableList()
    {
        possibleTables.Clear();
    }

    public void ChangeTable(Table table)
    {
        currentTable = table;
        possibleAnswers.Clear();
        foreach (Equation s in currentTable.GetTable())
        {
            possibleAnswers.Add(s.Equals());
        }
    }
    public Equation SetUpNewEquation()
    {
        ChangeTable(possibleTables[UnityEngine.Random.Range(0,possibleTables.Count)]);
        currentEquation = currentTable.GetTable()[UnityEngine.Random.Range(0, currentTable.GetTable().Count)];
        correctAnswer = currentEquation.Equals();
        //Debug.Log("Current question: " + currentSum.ToString() + "  " + correctAnswer);

        if(UnityEngine.Random.Range(0, 2) == 1)
        {
            RandomiseAnswer();
        }
        else
        {
            CorrectAnswer();
        }
        
        return currentEquation;
    }

    public void CorrectAnswer()
    {
        currentEquation.Answer = currentEquation.Equals();
    }

    public void RandomiseAnswer()
    {
        //currentCheckAnswer = possibleAnswers[UnityEngine.Random.Range(0, possibleAnswers.Count)];
        currentEquation.Answer = Convert.ToInt32(possibleAnswers[UnityEngine.Random.Range(0, possibleAnswers.Count)]);
        //Debug.Log("Checking: "+ currentCheckAnswer);
    }
}
