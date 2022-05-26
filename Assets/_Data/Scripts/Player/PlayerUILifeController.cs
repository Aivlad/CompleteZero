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
    /// »зменить количество €чеек в UI
    /// </summary>
    public void ChangeCountCell()
    {
        if (player != null)
        {
            // чистка зоны
            foreach (Transform child in playerHealthZone.transform)
                Destroy(child.gameObject);
            cells.Clear();

            // создание €чеек в зоне (на ней сетка дл€ упор€довачивани€)
            float countCell = player.healthMax / sizeCell;
            if (countCell > limitCountCell)
                countCell = limitCountCell;
            for (int i = 0; i < countCell; i++)
            {
                var newCell = Instantiate(cellLifePlayerPrefab);
                newCell.transform.SetParent(playerHealthZone.transform);
                cells.Add(newCell);
            }

            //сразу заполним
            ChangeValueCell();
        }
        else
        {
            // делаем поиск объекта player
            var playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.GetComponent<PlayerCharacteristics>();
                if (player != null)
                {
                    ChangeCountCell();
                }
                else
                    Debug.LogError("Ќа игроке нет PlayerCharacteristics");
            }
            else
                Debug.LogError("Ќе виден игрок на сцене");
        }
    }

    /// <summary>
    /// работаем с заполнением €чеек
    /// </summary>
    public void ChangeValueCell()
    {
        if (player != null)
        {
            float currentValue = player.health / sizeCell;
            // заполн€ем полные €чейки
            int wholePart = (int)currentValue;
            int i = 0;
            for (; i < wholePart; i++)
            {
                if (i > limitCountCell)
                    return;
                cells[i].transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
            }
            // неполна€ €чейка
            cells[i].transform.GetChild(0).GetComponent<Image>().fillAmount = currentValue - wholePart;
            i++;
            if (i > limitCountCell)
                return;
            // оставшиес€ пустые
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
