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
        bool triggered = restartInput.action.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

        if (triggered)
        {
            if(triggered != startedRestarting) { timeSinceButtonDown = 0; }

            Debug.Log("Timer incremeted");
            timeSinceButtonDown += Time.deltaTime;
            if (timeSinceButtonDown >= timeToTriggerRestart) 
            {
                Debug.Log("Restarted Scene");

                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }

        startedRestarting = triggered;
    }
}
