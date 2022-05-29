using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SimpleInventoryItem;

public class SimpleInventoryManager : MonoBehaviour
{
    private PlayerCharacteristics playerCharacteristics;
    private PlayerDamageController playerDamageController;

    private void Start()
    {
        // зона инвентаря (чистка)
        var inventoryZone = GameObject.FindGameObjectWithTag("SimpleInventoryZone");
        foreach (Transform child in inventoryZone.transform)
            Destroy(child.gameObject);


        // игрок
        var player = GameObject.Find("Player");
        if (player != null)
        {
            playerCharacteristics = player.GetComponent<PlayerCharacteristics>();
            playerDamageController = player.GetComponent<PlayerDamageController>();
        }
    }

    public void ItemAction(ItemType type)
    {
        switch (type)
        {
            case ItemType.question:
                Debug.Log("Вы подняли тестовый предмет");
                break;
            case ItemType.redСloak:
                IncreaseHealthPlayer(15);
                break;
            case ItemType.helmet:
                IncreaseHealthPlayer(20);
                break;
            case ItemType.boots:
                IncreaseHealthPlayer(20);
                break;
            case ItemType.armor:
                IncreaseHealthPlayer(25);
                break;
            case ItemType.pants:
                IncreaseHealthPlayer(25);
                break;
            case ItemType.blueСloak:
                IncreaseDamagePlayer(25);
                break;
            default:
                Debug.LogWarning("Не газначено событие для предмета");
                break;
        }
    }

    /// <summary>
    /// Увеличение max hp игрока
    /// </summary>
    /// <param name="magnificationAmount">насколько увеличитьы</param>
    private void IncreaseHealthPlayer(int magnificationAmount)
    {
        if (playerCharacteristics != null)
        {
            playerCharacteristics.IncreaseMaxHealth(magnificationAmount);
        }
    }

    /// <summary>
    /// Увеличение max hp игрока
    /// </summary>
    /// <param name="magnificationAmount">насколько увеличитьы</param>
    private void IncreaseDamagePlayer(int magnificationAmount)
    {
        if (playerDamageController != null)
        {
            playerDamageController.IncreaseDamage(15);
        }
    }
}
