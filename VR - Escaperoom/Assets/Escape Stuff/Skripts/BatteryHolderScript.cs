using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryHolderScript : MonoBehaviour
{
    
    Rigidbody batteryRig;
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Battery" && !other.gameObject.GetComponent<GrabbableObjectBehaviour>().isGrabbed)
        {
            batteryRig = other.gameObject.GetComponent<Rigidbody>();
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;
        }
    }
   


}
