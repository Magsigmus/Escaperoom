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
    public bool selected = false, lastselected = false;
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

        Rig = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    public void Update()
    {
        if(selected == lastselected) { return; }
        lastselected = selected;

        if (selected) {
            for (int i = 0; i < hiddenMaterials.Count; i++)
            {
                models[i].materials = hiddenMaterials[i];
            }
        }
        else
        {
            foreach (MeshRenderer mr in models)
            {
                hiddenMaterials.Add(mr.materials);
                Material[] whiteOuts = new Material[mr.materials.Length];
                for (int i = 0; i < whiteOuts.Length; i++) { whiteOuts[i] = whiteOut; }
                mr.materials = whiteOuts;
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
