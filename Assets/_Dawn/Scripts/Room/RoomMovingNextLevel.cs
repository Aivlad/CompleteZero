using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMovingNextLevel : MonoBehaviour
{
    private MenuController menuController;
    public string nameLoadingScene;
    [Space]
    public bool isStepOver = false;
    public string nameSceneConductor;

    private void Start()
    {
        menuController = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isStepOver)
            {
                menuController.StartScene(nameLoadingScene);
            }
            else
            {
                PlayerPrefs.SetString(KeysPlayerPrefs.SCENE_NAME_AFTER_VIDEO_DISPLAY, nameLoadingScene);
                menuController.StartScene(nameSceneConductor);
            }
        }
    }
}
