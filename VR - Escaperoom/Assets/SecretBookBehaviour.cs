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
            transform.eulerAngles = new Vector3(50, transform.eulerAngles.y, transform.eulerAngles.z);
            HingeJoint hingeJoint = GetComponent<HingeJoint>();
            JointLimits limit = hingeJoint.limits;
            limit.max = 50f;
            limit.min = 50f;
            GetComponent<HingeJoint>().limits = limit;
            reol.GetComponent<Animator>().SetTrigger("StartSlide");
        }
    }
}
