using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBlueSpiderController : MonoBehaviour
{
    public GameObject youngPrefab;
    public float cooldownSpawn;
    private float currentTime;

    private void Start()
    {
        currentTime = 0;
    }

    private void Update()
    {
        if (currentTime >= cooldownSpawn)
        {
            ActionNPC();
            currentTime = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    private void ActionNPC()
    {
        Instantiate(youngPrefab, transform.position, Quaternion.identity);
    }
}
