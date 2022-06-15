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
    [Space]
    public SimpleInventoryManager inventorySaveManager;

    private void Start()
    {
        menuController = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuController>();
        var inventoryObj = GameObject.FindGameObjectWithTag("SimpleInventoryManager");
        if (inventoryObj != null)
            inventorySaveManager = inventoryObj.GetComponent<SimpleInventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (inventorySaveManager != null)
            {
                inventorySaveManager.SaveInventory();
            }

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
