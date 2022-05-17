using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOpenPassages : MonoBehaviour
{
    public RoomController roomController;
    public RoomSpawnEnemies roomSpawnEnemies;
    public bool isDoorClosed;

    private void Start()
    {
        isDoorClosed = true;
    }

    private void Update()
    {
        // открываем двери
        if (isDoorClosed)
        {
            if (roomSpawnEnemies.enemiesAlive == false)
            {
                isDoorClosed = false;
                roomController.OpenAllDoors();
            }
        }
    }

}
