using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    [SerializeField] EquationController equationController;
    [SerializeField] List<Table> tables;

    public void EnableTable(int baseNr) 
    {
        tables[baseNr - 1].gameObject.SetActive(true);
    }

    public void ClearAllTables()
    {
        equationController.ClearTableList();
        foreach(Table t in tables)
        {
            t.gameObject.SetActive(false);
        }
    }
}
