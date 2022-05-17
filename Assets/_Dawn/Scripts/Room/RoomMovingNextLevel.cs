using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMovingNextLevel : MonoBehaviour
{
    private MenuController menuController;
    public string nameLoadingScene;

    private void Start()
    {
        menuController = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            menuController.StartScene(nameLoadingScene);
        }
    }
}
