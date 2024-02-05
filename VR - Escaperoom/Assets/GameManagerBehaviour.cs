using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PrimaryButtonEvent : UnityEngine.Events.UnityEvent<bool> { }

public class GameManagerBehaviour : MonoBehaviour
{
    public UnityEngine.InputSystem.InputActionReference restartInput;
    private bool lastButtonState = false;

    private void Update()
    {
        bool tempState = false;
        foreach (var device in devicesWithPrimaryButton)
        {
            Debug.Log(device.name);

            bool primaryButtonState = false;
            tempState = device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonState) // did get a value
                        && primaryButtonState // the value we got
                        || tempState; // cumulative result from other controllers
        }

        if (tempState != lastButtonState) // Button state changed since last frame
        {
            Debug.Log("CALLED THE EVENT!");
            primaryButtonPress.Invoke(tempState);
            lastButtonState = tempState;
        }
    }

    void InitializeInputDevices(UnityEngine.InputSystem.InputDevice arg1, UnityEngine.InputSystem.InputDeviceChange arg2)
    {
        var leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);

        foreach (var device in leftHandedControllers)
        {
            Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", device.name, device.characteristics.ToString()));
        }

        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        Debug.Log(allDevices.Count);

        foreach(InputDevice device in allDevices)
        {
            bool discardedValue;
            Debug.Log(device.name);
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out discardedValue))
            {
                devicesWithPrimaryButton.Add(device);
            }
        }
    }
}
