using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FlingToPoint : MonoBehaviour
{
    public float time = 1f;
    public float top = 0.25f;
    public bool grabing = false;
    public InputActionReference trigger;
    GameObject objToBeFling;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        if (!grabing && Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            var selected = hit.transform;
            objToBeFling = selected.gameObject;
            rb = objToBeFling.GetComponent<Rigidbody>();
            if (objToBeFling.GetComponent<XRGrabInteractable>() != null && rb != null && trigger.action.triggered)
            {
                rb.velocity = new Vector3(ForceToThis("x", objToBeFling), 1f, ForceToThis("z", objToBeFling));
            }
        }
    }


    float ForceToThis(string axis, GameObject objToBeFling)
    {
        if (axis == "x")
        {
            return time / this.transform.position.x - objToBeFling.transform.position.x;
        }
        else if (axis == "z")
        {
            return time / this.transform.position.x - objToBeFling.transform.position.x;
        }
        else if (axis == "y") // nont use yet need to think of gravaty
        {
            return this.transform.position.x - objToBeFling.transform.position.x;
        }
        return 1f;
    }

    public void grab() { grabing = true; }
    public void release() { grabing = false; }


}
