using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCNestController : MonoBehaviour
{
    public GameObject youngPrefab;
    public int countYoungSpawn;
    public float cooldownSpawn;
    public float scatteringRadius;
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
        Vector3 center = transform.position;
        float angleRotation = 360 / countYoungSpawn;
        for (int i = 0; i < countYoungSpawn; i++)
        {
            Vector3 pos = RandomCircle(center, scatteringRadius);
            Instantiate(youngPrefab, pos, Quaternion.identity);
        }
    }



    // случайный разброс по контуру круга
    private Vector3 RandomCircle(Vector3 center, float radius)
    {
        float angle = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}
