using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInflictDamageOnPlayer : MonoBehaviour
{
    public SceneManagerNPCState.TypesOfEnemies type;
    public SceneManagerNPCState sceneManager;
    [Space]
    public float damage;

    private void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerNPCState>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            damage = sceneManager.GetDamageMelee(type);
            collision.GetComponent<PlayerCharacteristics>()?.DealDamage(damage);
        }
    }
}
