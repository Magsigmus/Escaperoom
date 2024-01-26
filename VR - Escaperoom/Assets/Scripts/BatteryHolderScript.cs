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
        if (!snapUpdate) { return; }

        // IF REMOVED   
        if (objSnapped && grabObjBehv.isGrabbed)
        {
            objSnapped = false;
            unPowered.Invoke();
        }
        else if (!grabObjBehv.isGrabbed)
        {
            objRig.isKinematic = true;
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            powered.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != theTag) { return; }

        snapUpdate = true;
        obj = other;
        objRig = obj.gameObject.GetComponent<Rigidbody>();
        grabObjBehv = other.gameObject.GetComponent<GrabbableObjectBehaviour>();

        if (!grabObjBehv.isGrabbed)
        {
            objSnapped = true;
            powered.Invoke();
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
