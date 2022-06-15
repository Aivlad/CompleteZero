using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyController : MonoBehaviour
{
    [HideInInspector]public RoomSpawnEnemies roomSpawnEnemies;

    private void OnDestroy()
    {
        if (roomSpawnEnemies != null)
            roomSpawnEnemies.RemoveFromLiveList(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CenterRoom"))
        {
            roomSpawnEnemies = collision.GetComponent<RoomSpawnEnemies>();
        }
    }
}
