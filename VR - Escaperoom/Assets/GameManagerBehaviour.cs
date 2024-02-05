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
    public float timeSinceButtonDown = 0;
    public float timeToTriggerRestart = 1; 

    private void Update()
    {
        restartInput.action.started += StartedRestart;
        restartInput.action.canceled += CanceledRestart;

        if (startedRestarting)
        {
            Debug.Log("Timer incremeted");
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

        if(!startedRestarting){ Debug.Log("Restarted Timer"); timeSinceButtonDown = 0; }
        Debug.Log("Button Down!");
        startedRestarting = true;
        
    }

    private void CanceledRestart(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        startedRestarting = false;
    }
}
