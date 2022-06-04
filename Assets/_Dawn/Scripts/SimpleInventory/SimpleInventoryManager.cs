using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static SimpleInventoryItem;
using static SimpleInventorySave;
using System.Linq;

public class SimpleInventoryManager : MonoBehaviour
{
    private GameObject player;
    private PlayerCharacteristics playerCharacteristics;
    private PlayerDamageController playerDamageController;
    private PlayerMovement playerMovement;

    [Space]
    public string nameSaveFile = "SimpleInventory";
    private SpawnRoomController spawnRoomController;
    private Transform transformPlayer;
    private List<SaveItemsSimpleInvemtory> pickedUpItems;

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

        //for save
        if (player != null)
        {
            var spawnRoomControllerObj = GameObject.FindGameObjectWithTag("SpawnRoomController");
            if (spawnRoomControllerObj != null)
            {
                spawnRoomController = spawnRoomControllerObj.GetComponent<SpawnRoomController>();
            }
            pickedUpItems = new List<SaveItemsSimpleInvemtory>();
            transformPlayer = player.transform;
            StartCoroutine(LateSatrt());
        }        
    }

    private IEnumerator LateSatrt()
    {
        yield return new WaitForSeconds(0.7f);
        LoadInventory();
    }


    public void ItemAction(ItemType type)
    {
        //save
        int countPicked = (
                from pui in pickedUpItems
                where pui.type == type
                select pui
            ).Count();
        if (countPicked == 0)
        {
            pickedUpItems.Add(new SaveItemsSimpleInvemtory(type));
        }
        else if (countPicked == 1)
        {
            foreach (var item in pickedUpItems)
            {
                if (item.type == type)
                {
                    item.countItem++;
                    break;
                }
            }
        }
        else
            Debug.LogError("���-�� ����� �� ���");


        //action
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
        // �������� ����������
        if (!Directory.Exists(Application.persistentDataPath + "/Save"))
        {
            Directory.CreateDirectory((Application.persistentDataPath + "/Save"));
        }

        //set data
        SimpleInventorySave saveData = new SimpleInventorySave();
        saveData.SetData(pickedUpItems.ToArray());

        //����������
        File.WriteAllText(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json", JsonUtility.ToJson(saveData));
    }

    public void LoadInventory()
    {
        if (!File.Exists(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json"))
        {
            Debug.Log("���������� �� �������");
            return;
        }

        SimpleInventorySave saveData = new SimpleInventorySave();
        saveData = JsonUtility.FromJson<SimpleInventorySave>(File.ReadAllText(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json"));

        var typesItems = spawnRoomController.typeItems;
        pickedUpItems.Clear();
        var saveItems = saveData.GetData();
        for (int i = 0; i < saveItems.Length; i++)
        {
            foreach (var type in typesItems)
            {
                var availableType = type.GetComponent<SimpleInventoryItem>().type;
                if (saveItems[i].type == availableType)
                {
                    for (int j = 0; j < saveItems[i].countItem; j++)
                    {
                        Instantiate(type, transformPlayer.position, Quaternion.identity);
                    }
                }
            }
        }
        //foreach (var type in typesItems)
        //{
        //    Debug.Log(type.GetComponent<SimpleInventoryItem>().type);
        //    Instantiate(type, transformPlayer.position, Quaternion.identity);
        //}
    }
}
