using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static SimpleInventoryItem;
using static SimpleInventorySave;

public class SimpleInventoryManager : MonoBehaviour
{
    private GameObject player;
    private PlayerCharacteristics playerCharacteristics;
    private PlayerDamageController playerDamageController;
    private PlayerMovement playerMovement;

    [Space]
    private SaveItemsSimpleInvemtory saveData = new SaveItemsSimpleInvemtory();
    public string nameSaveFile = "SimpleInventory";
    private SpawnRoomController spawnRoomController;

    private void Start()
    {
        // зона инвентаря (чистка)
        var inventoryZone = GameObject.FindGameObjectWithTag("SimpleInventoryZone");
        foreach (Transform child in inventoryZone.transform)
            Destroy(child.gameObject);


        // игрок
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerCharacteristics = player.GetComponent<PlayerCharacteristics>();
            playerDamageController = player.GetComponent<PlayerDamageController>();
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        var spawnRoomControllerObj = GameObject.FindGameObjectWithTag("SpawnRoomController");
        if (spawnRoomControllerObj != null)
        {
            spawnRoomController = spawnRoomControllerObj.GetComponent<SpawnRoomController>();
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
            case ItemType.pike:
                ActicatedAdditionBleedingEffect();
                break;
            case ItemType.torch:
                ActicatedAdditionFireEffect();
                break;
            case ItemType.greenVial:
                ActicatedAdditionPoisoningEffect();
                break;
            case ItemType.rabbitFoot:
                ActivateEvasion(20);
                break;
            case ItemType.socks:
                IncreaseMovementSpeedPlayer(5);
                break;
            case ItemType.bootsWithWings:
                EnableLevitationOverPits();
                break;
            case ItemType.folio:
                CallLightning();
                break;
            case ItemType.soup:
                TreatmentPlayer(15);
                break;
            default:
                Debug.LogWarning("Не назначено событие для предмета");
                break;
        }
    }

    /// <summary>
    /// Отхилить игрока
    /// </summary>
    private void TreatmentPlayer(float value)
    {
        if (playerCharacteristics != null)
        {
            playerCharacteristics.Treatment(value);
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

    /// <summary>
    /// Вызвать возможность активации доп. эффекта кровотечения
    /// </summary>
    private void ActicatedAdditionBleedingEffect()
    {
        if (playerDamageController != null)
        {
            playerDamageController.isAdditionBleedingEffect = true;
        }
    }

    /// <summary>
    /// Вызвать возможность активации доп. эффекта огня
    /// </summary>
    private void ActicatedAdditionFireEffect()
    {
        if (playerDamageController != null)
        {
            playerDamageController.isAdditionFireEffect = true;
        }
    }

    /// <summary>
    /// Вызвать возможность активации доп. эффекта отравление
    /// </summary>
    private void ActicatedAdditionPoisoningEffect()
    {
        if (playerDamageController != null)
        {
            playerDamageController.isAdditionPoisoningEffect = true;
        }
    }

    /// <summary>
    /// Активация уклонение
    /// </summary>
    /// <param name="evasionPercentage">какой шанс уклонения поставить</param>
    private void ActivateEvasion(float evasionPercentage)
    {
        if (playerCharacteristics != null)
        {
            playerCharacteristics.dodgeСhance = evasionPercentage;
        }
    }

    /// <summary>
    /// Увеличить скорость перемещения игрока
    /// </summary>
    /// <param name="percentageIncreaseValue">на сколько процентов увеличить</param>
    public void IncreaseMovementSpeedPlayer(float percentageIncreaseValue)
    {
        if (playerMovement != null)
        {
            playerMovement.IncreaseMovementSpeed(percentageIncreaseValue);
        }
    }

    /// <summary>
    /// Включить парение над ямами
    /// </summary>
    public void EnableLevitationOverPits()
    {
        if (playerMovement != null)
        {
            playerMovement.isLevitationOverPits = true;
        }
    }

    /// <summary>
    /// Активировать призыв молний
    /// </summary>
    private void CallLightning()
    {
        if (player != null)
        {
            player.GetComponent<MechanicLightningStrikeOnEnemyInRoom>().enabled = true;
        }
    }

    public void SaveInventory()
    {
        //// проверка директории
        //if (!Directory.Exists(Application.persistentDataPath + "/Save"))
        //{
        //    Directory.CreateDirectory((Application.persistentDataPath + "/Save"));
        //}

        ////test
        //saveData.isArmor = true;

        ////сохранение
        //File.WriteAllText(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json", JsonUtility.ToJson(saveData));

    }

    public void LoadInventory()
    {
        //if (!File.Exists(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json"))
        //{
        //    Debug.Log("Сохранение не найдено");
        //    return;
        //}

        //saveData = JsonUtility.FromJson<SaveItemsSimpleInvemtory>(File.ReadAllText(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json"));
        //Debug.Log("Загрузка выполнена");

        //var typesItems = spawnRoomController.typeItems;
    }
}
