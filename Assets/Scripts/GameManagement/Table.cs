using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField, Range(0,10)] int baseNumber = 2;
    [SerializeField] List<Equation> equations;

    private void Awake()
    {
        Refresh();
    }

    public void Refresh()
    {
        GenerateTable(baseNumber);
    }

    public void GenerateTable(int basis)
    {
        equations.Clear();
        for(int i = 1; i < 11; i++)
        {
            equations.Add(new Equation(i, Operator.Multiply, basis));
        }
    }

    public List<Equation> GetTable()
    {
        return equations;
    }
}
