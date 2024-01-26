using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public bool locked = false;
    public HingeJoint doorJoint;
    private float angleMax, angleMin;

    void Start()
    {
        angleMax = doorJoint.limits.max;
        angleMin = doorJoint.limits.min;

        UpdateLockedState(locked);
    }

    public void UpdateLockedState(bool uLocked)
    {
        locked = uLocked;
        if (locked) 
        {
            JointLimits limit = doorJoint.limits;
            limit.max = 0f;
            limit.min = 0f;
            doorJoint.limits = limit;
        }
        else
        {
            JointLimits limit = doorJoint.limits;
            limit.max = angleMax;
            limit.min = angleMin;
            doorJoint.limits = limit;
        }
    }
}
