using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeadgearManagerBehaviour : MonoBehaviour
{
    public GameObject currentHeadgear;
    public MeshRenderer[] revealedModels;
    public bool hideOnEquip = true;

    // Start is called before the first frame update
    void Start()
    {
        List<MeshRenderer> newRevealedModels = GameObject.FindGameObjectsWithTag("HiddenText").Select(e => e.GetComponent<MeshRenderer>()).ToList();
        foreach (MeshRenderer mr in revealedModels) { newRevealedModels.Add(mr); }
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
            foreach(MeshRenderer mr in revealedModels) { mr.enabled = true; }
        }
    }
}
