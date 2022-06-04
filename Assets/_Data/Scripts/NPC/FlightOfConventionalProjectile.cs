using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightOfConventionalProjectile : MonoBehaviour
{
    public float damage;
    public float airspeed;
    public float destroyDistance;
    public Vector3 flightVector;
    private Vector3 spawnPosition;


    private void Start()
    {
        spawnPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(flightVector * airspeed * Time.deltaTime);
        if (Vector3.Distance(spawnPosition, transform.position) > destroyDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCharacteristics>()?.DealDamage(damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Wall"))
        {
            //Debug.Log("Стена колайдер");
            Destroy(gameObject);
        }

    }

}
