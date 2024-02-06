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
    public GameObject lightObj;
    public Color hoverColor;
    public Color celectedColor;
    public Color readyColor;
    public float startIntens = 1f;
    public float endIntens = 5f;
    public float neededSpeedMultyplier = 1f;
    Light light;
    GameObject objToBeFling;
    Rigidbody flingRb;
    Rigidbody rb;
    bool celect = false;
    Vector3 neededSpeed;
    Vector3 lastCord;
    Vector3 curentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        light = lightObj.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!grabing && Physics.Raycast(transform.position, transform.forward, out RaycastHit hit) /* && !celect */)
        {
            Debug.DrawRay(transform.position, transform.forward);

            objToBeFling = hit.transform.gameObject;
            flingRb = objToBeFling.GetComponent<Rigidbody>();

            if (objToBeFling.GetComponent<XRGrabInteractable>() != null && flingRb != null)
            {
                light.enabled = true;
//                light.color = hoverColor;
//                light.intensity = startIntens;
                lightObj.transform.position = objToBeFling.transform.position;
                if (trigger.action.triggered)
                {
//                    celect = true;
//                    neededSpeed = (objToBeFling.transform.position - this.transform.position) / 1000 * neededSpeedMultyplier;
//                    lastCord = transform.position;
                    flingRb.velocity = new Vector3(ForceToThis("x", objToBeFling), ForceToThis("y", objToBeFling), ForceToThis("z", objToBeFling));
                }
            }
        }
/*        else if (celect)
        {
            lightObj.transform.position = objToBeFling.transform.position;
            curentSpeed = transform.position - lastCord;

            if (signCheck())
            {
                light.intensity = startIntens + IntenetyCordCalc("x") + IntenetyCordCalc("y") + IntenetyCordCalc("z");
                if (speedCheck())
                {
                    light.color = readyColor;
                    if (trigger.action.phase == InputActionPhase.Waiting) { flingRb.velocity = new Vector3(ForceToThis("x", objToBeFling), ForceToThis("y", objToBeFling), ForceToThis("z", objToBeFling)); }
                }
            }
            else { light.intensity = startIntens; }
            if (trigger.action.phase == InputActionPhase.Waiting)
            {
                celect = false;
            }
            lastCord = transform.position;
        }*/
        else
        {
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
    float IntenetyCordCalc(string axis)
    {
        if (axis == "x")
        {
            return (Mathf.Abs(transform.position.x) - Mathf.Abs(lastCord.x) / neededSpeed.x * (endIntens - startIntens));
        }
        else if (axis == "y")
        {
            return (Mathf.Abs(transform.position.y) - Mathf.Abs(lastCord.y) / neededSpeed.y * (endIntens - startIntens));
        }
        else
        {
            return (Mathf.Abs(transform.position.z) - Mathf.Abs(lastCord.z) / neededSpeed.z * (endIntens - startIntens));
        }
    }
    bool signCheck()
    {
            return Mathf.Sign(lastCord.x - transform.position.x) == Mathf.Sign(neededSpeed.x) &&
            Mathf.Sign(lastCord.y - transform.position.y) == Mathf.Sign(neededSpeed.y) &&
            Mathf.Sign(lastCord.z - transform.position.z) == Mathf.Sign(neededSpeed.z);
    }
    bool speedCheck()
    {
        return Mathf.Abs(curentSpeed.x) > Mathf.Abs(neededSpeed.x)
        && Mathf.Abs(curentSpeed.x) > Mathf.Abs(neededSpeed.x)
        && Mathf.Abs(curentSpeed.x) > Mathf.Abs(neededSpeed.x);
    }


    public void grab() { grabing = true; }
    public void release() { grabing = false; }


}
