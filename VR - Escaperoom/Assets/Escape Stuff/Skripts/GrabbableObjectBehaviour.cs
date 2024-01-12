using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObjectBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    
    public bool isGrabbed = false;
    void Start()
    {
        
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
}
