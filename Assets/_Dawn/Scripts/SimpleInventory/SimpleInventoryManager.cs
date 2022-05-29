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
        // ���� ��������� (������)
        var inventoryZone = GameObject.FindGameObjectWithTag("SimpleInventoryZone");
        foreach (Transform child in inventoryZone.transform)
            Destroy(child.gameObject);


        // �����
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
            default:
                Debug.LogWarning("�� ��������� ������� ��� ��������");
                break;
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
}
