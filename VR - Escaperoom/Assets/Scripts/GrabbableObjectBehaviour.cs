using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObjectBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isGrabbed = false;
    public GameObject modelParent;
    public MeshRenderer[] models;
    public Transform newParent;
    Rigidbody Rig;

    void Start()
    {
        if(modelParent != null)
        {
            models = modelParent.GetComponentsInChildren<MeshRenderer>();
        }
        Rig = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    

    public void Grabbed()
    {
        isGrabbed = true;
    }

    public void Released()
    {
        transform.parent = newParent;
        isGrabbed = false;
        Rig.isKinematic = false;
    }

    public void Hide()
    {
        foreach(MeshRenderer mr in models) { mr.enabled = false; }
    }

    public void Show()
    {
        foreach (MeshRenderer mr in models) { mr.enabled = true; }
    }
}
