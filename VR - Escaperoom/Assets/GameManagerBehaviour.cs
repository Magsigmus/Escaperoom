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

    }
}
