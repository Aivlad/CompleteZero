using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static PlayerMovement;

public class InventoryCurrentCell : MonoBehaviour
{
    [HideInInspector]
    public int index;

    private GameObject inventoryObject;
    private InventoryController inventory;

    private GameObject player;
    private Transform playerTransform;
    private PlayerMovement playerMovement;

    private float offset = 1;

    private void Start()
    {
        inventoryObject = GameObject.FindGameObjectWithTag("InventoryManager");
        inventory = inventoryObject.GetComponent<InventoryController>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (inventory.items[index].customEvent != null)
            {
                inventory.items[index].customEvent.Invoke();
            }
            if (inventory.items[index].isRemovable)
            {
                RemoveItem();
            }
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (inventory.items[index].id != 0)
            {
                if (inventory.items[index].isDroped)
                {
                    DropItem();
                    RemoveItem();
                }
            }
        }
    }

    private void DropItem()
    {
        for (int i = 0; i < inventory.database.transform.childCount; i++)
        {
            InventoryItem item = inventory.database.transform.GetChild(i).GetComponent<InventoryItem>();
            if (item)
            {
                if (inventory.items[index].id == item.id)
                {
                    GameObject dropedObject = Instantiate(item.gameObject);
                    Facing facing = playerMovement.GetFacing();
                    switch (facing)
                    {
                        case Facing.UP:
                            dropedObject.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + offset, playerTransform.position.z);
                            break;
                        case Facing.LEFT:
                            dropedObject.transform.position = new Vector3(playerTransform.position.x - offset, playerTransform.position.y, playerTransform.position.z);
                            break;
                        case Facing.RIGHT:
                            dropedObject.transform.position = new Vector3(playerTransform.position.x + offset, playerTransform.position.y, playerTransform.position.z);
                            break;
                        case Facing.DOWN:
                            dropedObject.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - offset, playerTransform.position.z);
                            break;
                    }
                }
            }
        }
    }

    private void RemoveItem()
    {
        if (inventory.items[index].countItem > 1)
        {
            inventory.items[index].countItem--;
        }
        else
        {
            inventory.items[index] = new InventoryItem();
        }
        inventory.DisplayItems();
    }
}
