using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquationController : MonoBehaviour
{
    [SerializeField] private List<Table> possibleTables;

    [SerializeField] private Equation currentEquation;
    [SerializeField] private float correctAnswer;
    [SerializeField] private float currentCheckAnswer;

    [SerializeField] private List<float> possibleAnswers;

    private void Start()
    {
        SetUpNewEquation();
    }
    public Equation SetUpNewEquation()
    {
        Table t = possibleTables[UnityEngine.Random.Range(0, possibleTables.Count)];
        currentEquation = t.GetTable()[UnityEngine.Random.Range(0, t.GetTable().Count)];
        correctAnswer = currentEquation.Equals();
        possibleAnswers.Clear();
        foreach (Equation s in t.GetTable())
        {
            possibleAnswers.Add(s.Equals());
        }
        //Debug.Log("Current question: " + currentSum.ToString() + "  " + correctAnswer);
        UpdateAnswerToCheck();

        return currentEquation;
    }

    public void UpdateAnswerToCheck()
    {
        currentCheckAnswer = possibleAnswers[UnityEngine.Random.Range(0, possibleAnswers.Count)];
        //Debug.Log("Checking: "+ currentCheckAnswer);
    }
}
