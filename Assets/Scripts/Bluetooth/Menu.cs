using Bluetooth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private BTManager btManager;

    [SerializeField] Button[] mainMenuButtons;
    [SerializeField] Button backButton;

    GameObject dialog = null;

    private void Start()
    {

        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            dialog = new GameObject();
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void DisableMenuButtons()
    {
        foreach (Button btn in mainMenuButtons)
        {
            btn.gameObject.SetActive(false);
        }

        backButton.gameObject.SetActive(true);
    }


    public void PlayerStart()
    {
        DisableMenuButtons();

        Debug.Log("Started bluetooth monitoring");
        btManager.StartMonitoring();
    }

    public void GoalDeviceStart()
    {
        DisableMenuButtons();

        Debug.Log("Started advertising");
        btManager.StartAdvertising();
    }

    public void Back()
    {
        foreach (Button btn in mainMenuButtons)
        {
            btn.gameObject.SetActive(true);
        }

        if (btManager.IsMonitoring()) btManager.StopMonitoring();
        if (btManager.IsAdvertising()) btManager.StopAdvertising();

        backButton.gameObject.SetActive(false);

        StopAllCoroutines();

    }
}
