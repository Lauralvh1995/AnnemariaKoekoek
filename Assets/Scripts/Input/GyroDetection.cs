using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GyroDetection : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    [SerializeField]
    private Camera MainCamera;

    private float shakeSpeed = 0.2f;

    //private List<Quaternion> measurements;

    //private float x_old;
    //private float y_old;
    //private float z_old;

    //private float x;
    //private float y;
    //private float z;


    private void Start()
    {
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }
        return false;
    }

     private void Update()
        {
        if (gyroEnabled)
        {
            if (Input.gyro.rotationRateUnbiased.x > shakeSpeed || Input.gyro.rotationRateUnbiased.y > shakeSpeed || Input.gyro.rotationRateUnbiased.z > shakeSpeed)
            {
                MainCamera.backgroundColor = Color.red;
        }
        }

    }

        //private void Update()
        //{
        //    if (gyroEnabled)
        //    {
        //        if (measurements.Count >= 20)
        //        {
        //            if (measurements.Count > 21)
        //            {
        //                measurements.RemoveAt(0);
        //            }

        //            x_old = 0;
        //            y_old = 0;
        //            z_old = 0;

        //            x = 0;
        //            y = 0;
        //            z = 0;

        //            for (int i = 0; i < 10; i++)
        //            {
        //                x_old += measurements[i].x;
        //                y_old += measurements[i].y;
        //                z_old += measurements[i].z;
        //            }

        //            for (int i = 10; i < 20; i++)
        //            {
        //                x += measurements[i].x;
        //                y += measurements[i].y;
        //                z += measurements[i].z;
        //            }

        //            x_old = x_old / 10;
        //            y_old = y_old / 10;
        //            z_old = z_old / 10;

        //            x = x / 10;
        //            y = y / 10;
        //            z = z / 10;

        //            if (Math.Abs(x_old - x) > 0.1f || Math.Abs(y_old - y) > 0.1f || Math.Abs(z_old - z) > 0.1f)
        //            {
        //                MainCamera.backgroundColor = Color.red;
        //            }
        //        }
        //        measurements.Add(gyro.attitude);
        //        print(gyro.attitude);
        //    }
        //}
    }