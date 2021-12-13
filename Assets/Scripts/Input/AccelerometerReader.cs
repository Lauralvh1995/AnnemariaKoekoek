using UnityEngine;
using UnityEngine.InputSystem;

public class AccelerometerReader : MonoBehaviour
{
    [SerializeField] Vector3 currentAcceleration;
    [SerializeField] Vector3 previousAcceleration;
    [SerializeField] float currentMagnitude;
    private void OnEnable()
    {
        Debug.Log("Enabling accelerometer");
        InputSystem.EnableDevice(Accelerometer.current);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    private void OnDisable()
    {
        Debug.Log("Disabling accelerometer");
        InputSystem.DisableDevice(Accelerometer.current);
    }

    private void Update()
    {
        previousAcceleration = currentAcceleration;
        currentAcceleration = Accelerometer.current.acceleration.ReadValue();
        currentMagnitude = currentAcceleration.magnitude;

        Debug.Log("Current Acceleration: " + currentAcceleration + "\n" +
            "Current Magnitude: " + currentMagnitude + "\n" +
            "Acceleration delta: " + (previousAcceleration - currentAcceleration).ToString());
    }
    public Vector3 GetCurrentAcceleration()
    {
        return currentAcceleration;
    }
    public float GetCurrentMagnitude()
    {
        return currentMagnitude;
    }
    public Vector3 GetAccelerationDelta()
    {
        return previousAcceleration - currentAcceleration;
    }
}
