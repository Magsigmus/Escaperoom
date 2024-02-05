using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FlingToPoint : MonoBehaviour
{
    public float forceMultiplyer = 1f;
    public bool grabing = false;
    public InputActionReference trigger;
    public float gravityAdder = 5f;
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
            Debug.DrawRay(transform.position, transform.forward);

            objToBeFling = hit.transform.gameObject;
            rb = objToBeFling.GetComponent<Rigidbody>();
            if (objToBeFling.GetComponent<XRGrabInteractable>() != null && rb != null && trigger.action.triggered)
            {
                rb.velocity = new Vector3(ForceToThis("x", objToBeFling), ForceToThis("x", objToBeFling), ForceToThis("z", objToBeFling));
            }
        }
    }


    float ForceToThis(string axis, GameObject objToBeFling)
    {
        if (axis == "x")
        {
            return (this.transform.position.x - objToBeFling.transform.position.x) / forceMultiplyer;
        }
        else if (axis == "z")
        {
            return (this.transform.position.z - objToBeFling.transform.position.z) / forceMultiplyer;
        }
        else if (axis == "y") // nont use yet need to think of gravaty
        {
            return   gravityAdder + (this.transform.position.y - objToBeFling.transform.position.y) * forceMultiplyer;
        }
        return 12f;
    }

    public void grab() { grabing = true; }
    public void release() { grabing = false; }


}
