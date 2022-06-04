using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SimpleInventoryItem;

public class SimpleInventorySave
{
    public SaveItemsSimpleInvemtory[] pickedUpItems;

    public void SetData(SaveItemsSimpleInvemtory[] pickedUpItems)
    {
        this.pickedUpItems = pickedUpItems;
    }

    public SaveItemsSimpleInvemtory[] GetData()
    {
        return pickedUpItems;
    }



    [Serializable]
    public class SaveItemsSimpleInvemtory
    {
        public ItemType type;
        public int countItem;

        public SaveItemsSimpleInvemtory(ItemType type)
        {
            this.type = type;
            countItem = 1;
        }
    }
}
