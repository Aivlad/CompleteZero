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
        // ���� ��������� (������)
        var inventoryZone = GameObject.FindGameObjectWithTag("SimpleInventoryZone");
        foreach (Transform child in inventoryZone.transform)
            Destroy(child.gameObject);


        // �����
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
                Debug.Log("�� ������� �������� �������");
                break;
            case ItemType.red�loak:
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
            case ItemType.blue�loak:
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
                Debug.LogWarning("�� ��������� ������� ��� ��������");
                break;
        }
    }

    /// <summary>
    /// �������� ������
    /// </summary>
    private void TreatmentPlayer(float value)
    {
        if (playerCharacteristics != null)
        {
            playerCharacteristics.Treatment(value);
        }
    }

    /// <summary>
    /// ���������� max hp ������
    /// </summary>
    /// <param name="magnificationAmount">��������� ����������</param>
    private void IncreaseHealthPlayer(int magnificationAmount)
    {
        if (playerCharacteristics != null)
        {
            playerCharacteristics.IncreaseMaxHealth(magnificationAmount);
        }
    }

    /// <summary>
    /// ���������� max hp ������
    /// </summary>
    /// <param name="magnificationAmount">��������� ����������</param>
    private void IncreaseDamagePlayer(int magnificationAmount)
    {
        if (playerDamageController != null)
        {
            playerDamageController.IncreaseDamage(15);
        }
    }

    /// <summary>
    /// ������� ����������� ��������� ���. ������� ������������
    /// </summary>
    private void ActicatedAdditionBleedingEffect()
    {
        if (playerDamageController != null)
        {
            playerDamageController.isAdditionBleedingEffect = true;
        }
    }

    /// <summary>
    /// ������� ����������� ��������� ���. ������� ����
    /// </summary>
    private void ActicatedAdditionFireEffect()
    {
        if (playerDamageController != null)
        {
            playerDamageController.isAdditionFireEffect = true;
        }
    }

    /// <summary>
    /// ������� ����������� ��������� ���. ������� ����������
    /// </summary>
    private void ActicatedAdditionPoisoningEffect()
    {
        if (playerDamageController != null)
        {
            playerDamageController.isAdditionPoisoningEffect = true;
        }
    }

    /// <summary>
    /// ��������� ���������
    /// </summary>
    /// <param name="evasionPercentage">����� ���� ��������� ���������</param>
    private void ActivateEvasion(float evasionPercentage)
    {
        if (playerCharacteristics != null)
        {
            playerCharacteristics.dodge�hance = evasionPercentage;
        }
    }

    /// <summary>
    /// ��������� �������� ����������� ������
    /// </summary>
    /// <param name="percentageIncreaseValue">�� ������� ��������� ���������</param>
    public void IncreaseMovementSpeedPlayer(float percentageIncreaseValue)
    {
        if (playerMovement != null)
        {
            playerMovement.IncreaseMovementSpeed(percentageIncreaseValue);
        }
    }

    /// <summary>
    /// �������� ������� ��� �����
    /// </summary>
    public void EnableLevitationOverPits()
    {
        if (playerMovement != null)
        {
            playerMovement.isLevitationOverPits = true;
        }
    }

    /// <summary>
    /// ������������ ������ ������
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
        //// �������� ����������
        //if (!Directory.Exists(Application.persistentDataPath + "/Save"))
        //{
        //    Directory.CreateDirectory((Application.persistentDataPath + "/Save"));
        //}

        ////test
        //saveData.isArmor = true;

        ////����������
        //File.WriteAllText(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json", JsonUtility.ToJson(saveData));

    }

    public void LoadInventory()
    {
        //if (!File.Exists(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json"))
        //{
        //    Debug.Log("���������� �� �������");
        //    return;
        //}

        //saveData = JsonUtility.FromJson<SaveItemsSimpleInvemtory>(File.ReadAllText(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json"));
        //Debug.Log("�������� ���������");

        //var typesItems = spawnRoomController.typeItems;
    }
}
