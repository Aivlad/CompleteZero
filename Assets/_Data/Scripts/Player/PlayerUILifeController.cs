using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUILifeController : MonoBehaviour
{
    public GameObject playerHealthZone;
    public GameObject cellLifePlayerPrefab;
    public int sizeCell = 100;
    public int limitCountCell = 8;

    private PlayerCharacteristics player;
    public List<GameObject> cells;

    /// <summary>
    /// �������� ���������� ����� � UI
    /// </summary>
    public void ChangeCountCell()
    {
        if (player != null)
        {
            // ������ ����
            foreach (Transform child in playerHealthZone.transform)
                Destroy(child.gameObject);
            cells.Clear();

            // �������� ����� � ���� (�� ��� ����� ��� ����������������)
            float countCell = player.healthMax / sizeCell;
            if (countCell > limitCountCell)
                countCell = limitCountCell;
            for (int i = 0; i < countCell; i++)
            {
                var newCell = Instantiate(cellLifePlayerPrefab);
                newCell.transform.SetParent(playerHealthZone.transform);
                cells.Add(newCell);
            }

            //����� ��������
            ChangeValueCell();
        }
        else
        {
            // ������ ����� ������� player
            var playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.GetComponent<PlayerCharacteristics>();
                if (player != null)
                {
                    ChangeCountCell();
                }
                else
                    Debug.LogError("�� ������ ��� PlayerCharacteristics");
            }
            else
                Debug.LogError("�� ����� ����� �� �����");
        }
    }

    /// <summary>
    /// �������� � ����������� �����
    /// </summary>
    public void ChangeValueCell()
    {
        if (player != null)
        {
            float currentValue = player.health / sizeCell;
            // ��������� ������ ������
            int wholePart = (int)currentValue;
            int i = 0;
            for (; i < wholePart; i++)
            {
                if (i > limitCountCell)
                    return;
                cells[i].transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
            }
            // �������� ������
            cells[i].transform.GetChild(0).GetComponent<Image>().fillAmount = currentValue - wholePart;
            i++;
            if (i > limitCountCell)
                return;
            // ���������� ������
            for (; i < cells.Count; i++)
            {
                cells[i].transform.GetChild(0).GetComponent<Image>().fillAmount = 0f;
                if (i > limitCountCell)
                    return;
            }
            //Debug.Log($"currentValue = {currentValue}; wholePart = {wholePart}; i = {i}");

        }
    }
}
