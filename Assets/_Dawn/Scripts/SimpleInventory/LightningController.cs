using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    public float damage = 5f;
    public float timeDestroy = 1.3f;
    private void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<ObjectCharacteristics>().DealDamage(damage);
        }
    }
}
