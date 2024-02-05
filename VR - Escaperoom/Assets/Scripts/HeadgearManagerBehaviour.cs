using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeadgearManagerBehaviour : MonoBehaviour
{
    public GameObject currentHeadgear;
    public List<MeshRenderer> revealedModels;
    public bool hideOnEquip = true;
    public bool revealModels = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!revealModels) { return; }
        List<MeshRenderer> newRevealedModels = GameObject.FindGameObjectsWithTag("HiddenText").Select(e => e.GetComponent<MeshRenderer>()).ToList();
        foreach (MeshRenderer mr in newRevealedModels) { revealedModels.Add(mr); }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHeadgear != null)
        {
            currentHeadgear.transform.position = transform.position;
            currentHeadgear.transform.rotation = transform.rotation;

            if (currentHeadgear.GetComponent<GrabbableObjectBehaviour>().isGrabbed) 
            { 
                currentHeadgear.GetComponent<GrabbableObjectBehaviour>().Show();
                currentHeadgear = null;
                if (revealedModels.Count == 0) { return; }
                foreach (MeshRenderer mr in revealedModels) { mr.enabled = false; }
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        GrabbableObjectBehaviour grab = col.gameObject.GetComponent<GrabbableObjectBehaviour>();
        bool colIsGrabbed = ((grab == null) ? true : grab.isGrabbed);

        if (col.tag == "Headgear" && !colIsGrabbed)
        {
            currentHeadgear = col.gameObject;
            currentHeadgear.GetComponent<Rigidbody>().isKinematic = true;
            if (hideOnEquip) { grab.Hide(); }
            if(revealedModels.Count == 0) { return; }
            foreach(MeshRenderer mr in revealedModels) { mr.enabled = true; }
        }
    }
}
