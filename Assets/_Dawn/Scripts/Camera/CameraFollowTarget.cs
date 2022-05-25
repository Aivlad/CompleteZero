using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    private Transform target;
    public float distanceZ = -14;


    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + Vector3.forward * distanceZ;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

}
