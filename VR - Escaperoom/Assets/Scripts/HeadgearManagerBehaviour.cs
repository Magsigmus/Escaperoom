using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class HeadgearManagerBehaviour : MonoBehaviour
{
    public GameObject currentHeadgear;
    public List<Renderer> revealedModels;
    public bool hideOnEquip = true;
    public bool revealModels = false;
    public AudioSource equipSound;
    public Image equipOverlay;

    // Start is called before the first frame update
    void Start()
    {
        if (!revealModels) { return; }
        equipOverlay.enabled = false;
        List<Renderer> newRevealedModels = GameObject.FindGameObjectsWithTag("HiddenText").Select(e => e.GetComponent<Renderer>()).ToList();
        foreach (Renderer mr in newRevealedModels) { revealedModels.Add(mr); }
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
                foreach (Renderer mr in revealedModels) { mr.enabled = false; }
                if (revealModels) { equipOverlay.enabled = false; }
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
            if (hideOnEquip) { grab.Hide(); equipSound.Play(); }
            if(revealedModels.Count == 0) { return; }
            foreach(Renderer mr in revealedModels) { mr.enabled = true; }
            if (revealModels) { equipOverlay.enabled = true; }
        }
    }
}
