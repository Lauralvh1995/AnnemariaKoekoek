using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementChecker : MonoBehaviour
{
    [SerializeField]
    private GyroDetection gyroDetection;
    [SerializeField]
    private AccelDetection accelDetection;
    [SerializeField]
    private Camera MainCamera;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public bool IsPlayerMoving()
    {
        if(accelDetection.IsMoving() || gyroDetection.IsMoving())
        {
            return true;
        }
        return false;
    }
}
