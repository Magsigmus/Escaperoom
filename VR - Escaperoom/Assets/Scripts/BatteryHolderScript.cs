using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BatteryHolderScript : MonoBehaviour
{
    public string theTag;
    Rigidbody objRig;
    Collider obj;
    GrabbableObjectBehaviour grabObjBehv;
    bool snapUpdate = false;
    bool objSnapped = false;
    public UnityEvent powered;
    public UnityEvent unPowered;

    void Update()
    {
        if (snapUpdate)
        {
            if (objSnapped && grabObjBehv)
            {
                objSnapped = false;
                unPowered.Invoke();
            }
            if (grabObjBehv)
            {
                objRig.isKinematic = false;
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
                powered.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == theTag && !other.gameObject.GetComponent<GrabbableObjectBehaviour>().isGrabbed)
        {
            obj = other;
            objRig = other.gameObject.GetComponent<Rigidbody>();
            objRig.isKinematic = false;
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;
            snapUpdate = true;
            objSnapped = true;
            powered.Invoke();
        }
        else if (other.tag == theTag)
        {
            obj = other;
            grabObjBehv = other.gameObject.GetComponent<GrabbableObjectBehaviour>();
            objRig = obj.gameObject.GetComponent<Rigidbody>();
            snapUpdate = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == theTag)
        {
            snapUpdate = false;
        }
    }


}
