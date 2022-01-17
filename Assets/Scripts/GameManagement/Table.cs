using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Table : MonoBehaviour
{
    [SerializeField, Range(0,10)] int baseNumber = 2;
    [SerializeField] List<Equation> equations;

    [SerializeField] SetupTableEvent setupEvent;
    private void Awake()
    {
        Refresh();
    }
    private void OnEnable()
    {
        setupEvent.Invoke(this);
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
            equations.Add(new Equation(i, Equation.Operator.Keer, basis, i * basis));
        }
    }

    public List<Equation> GetTable()
    {
        return equations;
    }
}
[Serializable]
public class SetupTableEvent : UnityEvent<Table>
{

}
