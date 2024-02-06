using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SecretBookBehaviour : MonoBehaviour
{
    public GameObject reol;

    private void Update()
    {
        if(transform.eulerAngles.x < 55)
        {
            reol.GetComponent<Animator>().SetTrigger("StartSlide");
        }
    }
}
