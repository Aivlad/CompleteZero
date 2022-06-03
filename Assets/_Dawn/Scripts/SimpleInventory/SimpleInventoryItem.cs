using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleInventoryItem : MonoBehaviour
{
    [Header("Common parameters")]
    public GameObject UITemplateItemPrefab;
    public Sprite UISpriteItem;
    private GameObject inventoryZone;
    private SimpleInventoryManager inventoryManager;    

    public enum ItemType
    {
        none,
        question,
        redСloak,
        helmet,
        boots,
        armor,
        pants,
        blueСloak,
        pike,
        torch,
        greenVial,
        rabbitFoot,
        socks,
        bootsWithWings,
        folio,
        soup
    };
    public ItemType type;

    public enum WhatToDo
    {
        toInventory,
        useImmediately
    }
    public WhatToDo action = WhatToDo.toInventory;


    private void Start()
    {
        inventoryZone = GameObject.FindGameObjectWithTag("SimpleInventoryZone");
        var manager = GameObject.FindGameObjectWithTag("SimpleInventoryManager");
        if (manager != null)
        {
            inventoryManager = manager.GetComponent<SimpleInventoryManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (inventoryZone == null)
                Debug.LogWarning("Система простого инвентаря отключена");
            else
            {
                switch(action)
                {
                    case WhatToDo.useImmediately:
                        Eat();
                        break;
                    default:
                        GoIntoInventory();
                        break;
                }
            }
                
        }
    }

    /// <summary>
    /// Объект влезает в инвентарь
    /// </summary>
    public void GoIntoInventory()
    {
        // проверка на "есть ли место для визуализации"
        if (inventoryZone.transform.childCount < 55)
        {
            var newItem = Instantiate(UITemplateItemPrefab);
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = UISpriteItem;
            newItem.transform.SetParent(inventoryZone.transform);
        }
        inventoryManager.ItemAction(type);
        Destroy(gameObject);
    }

    /// <summary>
    /// Скушать предмет
    /// </summary>
    private void Eat()
    {
        inventoryManager.ItemAction(type);
        Destroy(gameObject);
    }

}
