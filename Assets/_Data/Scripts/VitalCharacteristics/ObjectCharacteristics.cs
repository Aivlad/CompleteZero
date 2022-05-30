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
    [Space]
    public bool isInRoom = false;


    private void Start()
    {
        // если есть SceneManagerNPCState, то берем хп от туда
        var sceneManagerObject = GameObject.FindGameObjectWithTag("SceneManager");
        if (sceneManagerObject != null)
        {
            sceneManager = sceneManagerObject.GetComponent<SceneManagerNPCState>();
            healthMax = sceneManager.GetHealth(type);
        }
        // если не было SceneManagerNPCState, то хп будут из инспектора (через VitalCharacteristics)
        health = healthMax;

        StartCoroutine(CheckBeingInRoom());
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("CenterRoom"))
        {
            isInRoom = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CenterRoom"))
        {
            DealDamage(999999);
        }
    }

    private IEnumerator CheckBeingInRoom()
    {
        yield return new WaitForSeconds(1.1f);
        if (!isInRoom)
            DealDamage(999999);
    }
}
