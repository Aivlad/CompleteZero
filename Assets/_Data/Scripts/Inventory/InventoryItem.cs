using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryItem : MonoBehaviour
{
    public string nameItem;
    public int id;
    [HideInInspector]
    public int countItem;
    public bool isStackable;
    [Multiline(5)]
    public string descriptionItem;

    public bool isRemovable;
    public bool isDroped;

    public Sprite icon;
    public UnityEvent customEvent;
}
