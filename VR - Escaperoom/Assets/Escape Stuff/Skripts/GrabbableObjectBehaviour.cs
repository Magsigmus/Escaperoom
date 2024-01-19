using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObjectBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isGrabbed = false;
    public GameObject modelParent;
    public MeshRenderer[] models;

    void Start()
    {
        if(modelParent != null)
        {
            models = modelParent.GetComponentsInChildren<MeshRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grabbed()
    {
        isGrabbed = true;
    }

    public void Released()
    {
        isGrabbed = false;
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
