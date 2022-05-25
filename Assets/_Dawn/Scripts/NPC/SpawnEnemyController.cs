using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyController : MonoBehaviour
{
    [HideInInspector]public RoomSpawnEnemies roomSpawnEnemies;

    private void OnDestroy()
    {
        roomSpawnEnemies.RemoveFromLiveList(gameObject);
    }
}
