using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [HideInInspector]
    public List<InventoryItem> items;
    public GameObject cellContainer;

    public GameObject database;

    public KeyCode showInventory;

    public bool isPaused;

    private void Start()
    {
        cellContainer.SetActive(false);

        items = new List<InventoryItem>();
        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            cellContainer.transform.GetChild(i).GetComponent<InventoryCurrentCell>().index = i;
            items.Add(new InventoryItem());
        }
    }

    private void Update()
    {
        ToggleInventory();
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void ToggleInventory()
    {
        if (Input.GetKeyDown(showInventory))
        {
            if (cellContainer.activeSelf)
            {
                cellContainer.SetActive(false);
            }
            else
            {
                cellContainer.SetActive(true);
            }
            isPaused = !isPaused;
        }
    }

    public void TakeItem(GameObject item)
    {
        if (item.GetComponent<InventoryItem>())
        {
            AddItem(item.GetComponent<InventoryItem>());
        }
    }

    private void AddItem(InventoryItem currentItem)
    {
        if (currentItem.isStackable)
        {
            AddStackableItem(currentItem);
        }
        else
        {
            AddUnstackableItem(currentItem);
        }
    }

    private void AddUnstackableItem(InventoryItem currentItem)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == 0)
            {
                items[i] = currentItem;
                items[i].countItem = 1;
                DisplayItems();
                Destroy(currentItem.gameObject);
                break;
            }
        }
    }

    private void AddStackableItem(InventoryItem currentItem)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == currentItem.id)
            {
                items[i].countItem++;
                DisplayItems();
                Destroy(currentItem.gameObject);
                return;
            }
        }
        AddUnstackableItem(currentItem);
    }

    public void DisplayItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Transform cell = cellContainer.transform.GetChild(i);
            Transform icon = cell.GetChild(0);
            Transform count = icon.GetChild(0);

            Text txt = count.GetComponent<Text>();
            Image img = icon.GetComponent<Image>();

            if (items[i].id != 0)
            {
                img.enabled = true;
                img.sprite = items[i].icon;
                if (items[i].countItem > 1)
                {
                    txt.text = items[i].countItem.ToString();
                }
                else
                {
                    txt.text = null;
                }
            }
            else
            {
                img.enabled = false;
                img.sprite = null;
                txt.text = null;
            }
        }
    }
}
