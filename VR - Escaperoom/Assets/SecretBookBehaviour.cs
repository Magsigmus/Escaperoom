using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SecretBookBehaviour : MonoBehaviour
{
    public GameObject reol;
    public AudioSource click;
    bool moved = false;

    private void Update()
    {
        if(transform.eulerAngles.x < 55 && !moved)
        {
            reol.GetComponent<Animator>().SetTrigger("StartSlide");
            reol.GetComponent<AudioSource>().Play();
            click.Play();
            moved = true;
        }
    }
}
