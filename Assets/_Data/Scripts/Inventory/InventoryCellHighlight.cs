using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCellHighlight : MonoBehaviour
{
    public Sprite activeCell;
    private Sprite defaultCell;
    private Image img;

    private void Start()
    {
        img = GetComponent<Image>();
        defaultCell = img.sprite;
    }

    private void OnDisable()
    {
        img.sprite = defaultCell;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.sprite = activeCell;
        Debug.Log("Наведение");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.sprite = defaultCell;
    }
}
