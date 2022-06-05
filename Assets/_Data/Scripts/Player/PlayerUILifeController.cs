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
    public void ChangeCountCell(PlayerCharacteristics player)
    {
        cells = new List<GameObject>();
        this.player = player;
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
                if (i >= limitCountCell)
                    return;
                cells[i].transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
            }
            // неполна€ €чейка
            var remainderValue = currentValue - wholePart;
            //Debug.Log($"count = {cells.Count} ; i = {i}");
            if (remainderValue > 0)
            {
                if (i >= limitCountCell)
                    return;
                cells[i].transform.GetChild(0).GetComponent<Image>().fillAmount = remainderValue;
            }
        }
    }
}
