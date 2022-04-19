using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCharacteristics : MonoBehaviour
{
    public float health;
    public float healthMax;
    [Space]
    public GameObject flyingDamagePrefab;
    public Vector3 offset;

    private void Start()
    {
        health = healthMax;
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
        CallFlyingDamage(damage);
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void CallFlyingDamage(float damage)
    {
        var inst = Instantiate(flyingDamagePrefab);
        inst.transform.parent = gameObject.transform;
        inst.transform.position = transform.position + offset;
        inst.GetComponent<FlyingDamage>().damage = damage;
        
    }
}
