using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPutIn : MonoBehaviour
{
    public InventoryController inventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InventoryItem"))
        {
            inventory.TakeItem(collision.gameObject);
        }
    }
}
