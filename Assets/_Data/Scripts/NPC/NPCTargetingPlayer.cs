using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTargetingPlayer : MonoBehaviour
{
    public Transform transformPlayer;
    public float offset;


    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            transformPlayer = player.transform;
    }

    private void Update()
    {
        if (transformPlayer != null)
        {
            Vector3 difference = transformPlayer.position - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        }
    }
}
