using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementChecker : MonoBehaviour
{
    GyroDetection gyroDetection;
    AccelDetection accelDetection;

    // Start is called before the first frame update
    void Start()
    {
        gyroDetection = new GyroDetection();
        accelDetection = new AccelDetection();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool IsPlayerMoving()
    {
        if(gyroDetection.IsMoving() && accelDetection.IsMoving())
        {
            return true;
        }
        return false;
    }
}
