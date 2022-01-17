using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LED : MonoBehaviour
{
    [SerializeField]
    private Image image;

    public void StartGame()
    {
        image.color = Color.green;
    }

    public void Lose()
    {
        image.color = Color.red;
    }

    public void Win()
    {
        image.color = Color.magenta;
    }
}
