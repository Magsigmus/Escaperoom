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
    public bool rightControllerPointing = false, leftControllerPointing = false;
    public Material whiteOut;
    List<Material[]> hiddenMaterials = new List<Material[]>(), whiteOutMaterials = new List<Material[]>();
    Rigidbody Rig;

    void Start()
    {
        if(modelParent != null)
        {
            models = modelParent.GetComponentsInChildren<MeshRenderer>();
        }
        else
        {
            models = GetComponentsInChildren<MeshRenderer>();
        }
        
        foreach (MeshRenderer mr in models)
        {
            hiddenMaterials.Add(mr.materials);
            Material[] whiteOuts = new Material[mr.materials.Length];
            for (int i = 0; i < whiteOuts.Length; i++) { whiteOuts[i] = whiteOut; }
            whiteOutMaterials.Add(whiteOuts);
        }

        Rig = GetComponent<Rigidbody>();
    }

    public void UpdateSelected(int change, bool setVal)
    {
        bool beforeChange = rightControllerPointing || leftControllerPointing;

        if (change == 1) { leftControllerPointing = setVal; } // Left Controller
        if (change == 2) { rightControllerPointing = setVal; } // Right Controller

        if (beforeChange == (rightControllerPointing || leftControllerPointing)) { return; }

        if (rightControllerPointing || leftControllerPointing)
        {
            for (int i = 0; i < hiddenMaterials.Count; i++)
            {
                models[i].materials = whiteOutMaterials[i];
            }
        }
        else
        {
            for (int i = 0; i < models.Length; i++)
            {
                models[i].materials = hiddenMaterials[i];
            }
        }
    }

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
