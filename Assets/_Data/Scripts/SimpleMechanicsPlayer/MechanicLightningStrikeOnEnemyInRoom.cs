using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicLightningStrikeOnEnemyInRoom : MonoBehaviour
{

    private GameObject currentRoom;
    public float cooldownAction;
    public float currentTime;
    public GameObject spawnObjectPrefab;

    private void Start()
    {
        currentTime = 0;
    }

    private void Update()
    {
        if (currentTime >= cooldownAction)
        {
            ActionMechanic();
            currentTime = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("CenterRoom"))
        {
            currentRoom = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CenterRoom"))
        {
            currentRoom = null;
        }
    }

    private void ActionMechanic()
    {
        if (currentRoom != null)
        {
            var enemies = currentRoom.GetComponent<RoomSpawnEnemies>().spawnedEnemies;
            if (enemies.Count > 0)
            {
                var index = Random.Range(0, enemies.Count);
                Instantiate(spawnObjectPrefab, enemies[index].transform.position, Quaternion.identity);
            }
        }
        
    }

}
