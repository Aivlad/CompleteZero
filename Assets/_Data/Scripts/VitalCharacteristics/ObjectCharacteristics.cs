using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCharacteristics : VitalCharacteristics
{
    public SceneManagerNPCState.TypesOfEnemies type;
    public SceneManagerNPCState sceneManager;
    [Space]
    public GameObject flyingDamagePrefab;
    public Vector3 offset;

    private void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerNPCState>();
        healthMax = sceneManager.GetHealth(type);

        health = healthMax;
    }

    public override void DealDamage(float damage)
    {
        CallFlyingDamage(damage);
        health -= damage;
        CheckDeath();
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

    public float GetHealthTotal()
    {
        return healthMax;
    }
}
