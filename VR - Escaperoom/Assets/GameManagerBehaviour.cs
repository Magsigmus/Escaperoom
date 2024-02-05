using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class PrimaryButtonEvent : UnityEngine.Events.UnityEvent<bool> { }

public class GameManagerBehaviour : MonoBehaviour
{
    public UnityEngine.InputSystem.InputActionReference restartInput;
    private bool startedRestarting = false;
    private float timeSinceButtonDown = 0;
    public float timeToTriggerRestart = 1; 

    private void Update()
    {
        restartInput.action.started += StartedRestart;
        restartInput.action.canceled += CanceledRestart;

        if (startedRestarting)
        {
            timeSinceButtonDown += Time.deltaTime;
            if (timeSinceButtonDown >= timeToTriggerRestart) 
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }

        startedRestarting = restartInput.action.triggered;
    }

    private void StartedRestart(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log("Button Down!");
        startedRestarting = true;
        timeSinceButtonDown = 0;
    }

    private void CanceledRestart(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        startedRestarting = false;
    }
}
