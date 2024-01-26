using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumPadButton : MonoBehaviour
{
    public string me;
    public NumPadManeger Maneger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finger")
        {
            Maneger.ButtonInput(me);
        }
    }
}
