using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPrinter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Triggered");
    }

}
