using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReolBehaviour : MonoBehaviour
{
    public List<GameObject> itemsInReol = new List<GameObject>();

    private void Update()
    {
        foreach(GameObject item in itemsInReol)
        {
            if(item.tag == "IgnoreReol") { continue; }

            if(gameObject == item) { continue; }

            Rigidbody rb = item.GetComponent<Rigidbody>();

            if(rb == null)
            {
                item.transform.parent = transform;
            }
            else if (rb.velocity.magnitude <= 0.1)
            {
                GrabbableObjectBehaviour grab = item.GetComponent<GrabbableObjectBehaviour>();
                if(grab == null)
                {
                    item.transform.SetParent(transform);
                }
                else if(!grab.isGrabbed)
                {
                    item.transform.SetParent(transform);
                }
                else
                {
                    grab.newParent = null;
                    item.transform.SetParent(null);
                }
            }
            else
            {
                item.transform.SetParent(null);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        itemsInReol.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name);

        other.gameObject.transform.SetParent(null);
        itemsInReol.Remove(other.gameObject);
    }
}
