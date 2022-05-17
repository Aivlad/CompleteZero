using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMainManager : MonoBehaviour
{
    public SpawnRoomController spawnRoomController;

    private void Start()
    {
        spawnRoomController.LevelGeneration();
    }
}
