using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FlingToPoint : MonoBehaviour
{
    public float forceMultiplyer = 2f;
    public float gravityAdder = 3f;
    public bool grabing = false;
    public InputActionReference trigger;
    public InputActionReference hold;
    public GameObject lightObj;
    public Transform rayStartPoint, rayDirPoint;
    public Material selectionMaterial;
    Light light;
    GameObject objToBeFling;
    Rigidbody flingRb;
    bool lastSelected = false;
    Vector3 neededSpeed;
    Vector3 lastCord;
    Vector3 curentSpeed;
    GrabbableObjectBehaviour grabBe = null;
    public int handIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        light = lightObj.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hold.action.phase != InputActionPhase.Waiting && !grabing && Physics.Raycast(rayStartPoint.position, rayDirPoint.position - rayStartPoint.position, out RaycastHit hit) /* && !celect */)
        {
            Debug.DrawRay(rayStartPoint.position, (rayDirPoint.position - rayStartPoint.position) * 100);

            bool newObj = objToBeFling != hit.transform.gameObject;

            objToBeFling = hit.transform.gameObject;
            flingRb = objToBeFling.GetComponent<Rigidbody>();

            if (objToBeFling.GetComponent<XRGrabInteractable>() != null && flingRb != null)
            {
                if (newObj && grabBe != null) { grabBe.UpdateSelected(handIndex, false); }

                grabBe = objToBeFling.GetComponent<GrabbableObjectBehaviour>();
                if(grabBe == null) { Debug.Log($"The object {objToBeFling.name} is able to be picked up, but is missing a GrabbableObjectBehaviour."); }
                else { grabBe.UpdateSelected(handIndex, true); }

                light.enabled = true;
                lightObj.transform.position = objToBeFling.transform.position;
                if (trigger.action.triggered)
                {
                    flingRb.velocity = new Vector3(ForceToThis("x", objToBeFling), ForceToThis("y", objToBeFling), ForceToThis("z", objToBeFling));
                }
            }
            else
            {
                if (grabBe != null) { grabBe.UpdateSelected(handIndex, false); }
                light.enabled = false;
            }
        }
        else
        {
            if(grabBe != null) { grabBe.UpdateSelected(handIndex, false); }
            light.enabled = false;
        }
    }

    float ForceToThis(string axis, GameObject objToBeFling)
    {
        if (axis == "x")
        {
            return (this.transform.position.x - objToBeFling.transform.position.x) * forceMultiplyer;
        }
        else if (axis == "z")
        {
            return (this.transform.position.z - objToBeFling.transform.position.z) * forceMultiplyer;
        }
        else if (axis == "y")
        {
            return   gravityAdder + (this.transform.position.y - objToBeFling.transform.position.y) * forceMultiplyer;
        }
        return 12f;
    }

    public void grab() { grabing = true; }
    public void release() { grabing = false; }


}
