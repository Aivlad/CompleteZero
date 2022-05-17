using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Transform centerRoom;
    [Header("Doors")]
    public GameObject DoorT;
    public GameObject DoorR;
    public GameObject DoorB;
    public GameObject DoorL;
    [HideInInspector] public GameObject neighborExitDoor = null;    // дверь-переход соседа в эту комнату
    [HideInInspector] public RoomController neighborExitRoom = null;    // сосед, из которого пришли в эту комнату

    [Header("Plugs")]
    public GameObject parentPlugs;
    public GameObject prefabPlug;

    [Header("Metadata")]
    private RoomControllerSave dataSave = new RoomControllerSave();
    private bool isLastRoom = true;

    /// <summary>
    /// Откыть все двери
    /// </summary>
    public void OpenAllDoors()
    {
        if (DoorT != null)
            DoorT.SetActive(false);
        if (DoorR != null)
            DoorR.SetActive(false);
        if (DoorB != null)
            DoorB.SetActive(false);
        if (DoorL != null)
            DoorL.SetActive(false);
    }

    /// <summary>
    /// Закрыть проходы дверями, а вместо дверей болванок повесить заглушки
    /// </summary>
    public void ClosePassagesAndOpenPlugs()
    {
        // для удобства формируем временный массив
        GameObject[] doors = new GameObject[4];
        doors[0] = DoorT;
        doors[1] = DoorR;
        doors[2] = DoorB;
        doors[3] = DoorL;

        // закрываем проходы дверьми, удаляем балванки и вешаем заглушки
        foreach (GameObject door in doors)
        {
            // уничтожаем неактивами и вешаем заглушки
            if (door.activeInHierarchy)
            {
                CloseDoorWithPlug(door);
                continue;
            }
            // неактивы (т.е. проходы) закрываем дверьми
            door.SetActive(true);
        }
    }

    /// <summary>
    /// Уничтожить дверь и повесить заглушку на ее место
    /// </summary>
    public void CloseDoorWithPlug(GameObject door)
    {
        var inst = Instantiate(prefabPlug, door.transform.position, door.transform.rotation);
        inst.transform.parent = parentPlugs.transform;
        Destroy(door);
    }

    /// <summary>
    /// Установить первичные данные генерации
    /// </summary>
    public void SetPrimaryData(int indexTypeRoom, int indexInMatrixI, int indexInMatrixJ)
    {
        dataSave.SetPrimaryData(indexTypeRoom, indexInMatrixI, indexInMatrixJ);
    }

    /// <summary>
    /// Установить активацию дверей (true - оставить, false - destroy)
    /// </summary>
    public void SetDoorsActivation(bool isDoorT, bool isDoorR, bool isDoorB, bool isDoorL)
    {
        if (!isDoorT)
        {
            CloseDoorWithPlug(DoorT);
        }
        else
        {
            DoorT.SetActive(true);
        }
        if (!isDoorR)
        {
            CloseDoorWithPlug(DoorR);
        }
        else
        {
            DoorR.SetActive(true);
        }
        if (!isDoorB)
        {
            CloseDoorWithPlug(DoorB);
        }
        else
        {
            DoorB.SetActive(true);
        }
        if (!isDoorL)
        {
            CloseDoorWithPlug(DoorL);
        }
        else
        {
            DoorL.SetActive(true);
        }
    }

    /// <summary>
    /// Является ли комнтата крайней
    /// </summary>
    public void SetIsLastRoom(bool isLast = false)
    {
        isLastRoom = isLast;
    }

    /// <summary>
    /// Получить isLastRoom
    /// </summary>
    public bool GetIsLastRoom()
    {
        return isLastRoom;
    }

    /// <summary>
    /// Получить объект для сохранения
    /// </summary>
    /// <returns>Объект сохранения</returns>
    public RoomControllerSave GetDataSave()
    {
        dataSave.SetPresenceDoors(this);
        dataSave.SetIsLastRoom(isLastRoom);
        return dataSave;
    }

    [Serializable]
    public class RoomControllerSave
    {
        public bool isNull; // для JSON, чтобы удобнее понимать наличие объекта

        public int indexTypeRoom; // индекс в списке типов (от сцены к сцене может меняться)

        public int indexInMatrixI;  // индекс i в матрице при генерации  (от сцены к сцене меняtся)
        public int indexInMatrixJ;  // индекс j в матрице при генерации  (от сцены к сцене меняtся)


        // наличие дверей
        public bool isDoorT;
        public bool isDoorR;
        public bool isDoorB;
        public bool isDoorL;

        // последняя комната или нет
        public bool isLastRoom;

        public RoomControllerSave()
        {
            isNull = true;
        }

        /// <summary>
        /// Установить первичные данные
        /// </summary
        public void SetPrimaryData(int indexTypeRoom, int indexInMatrixI, int indexInMatrixJ)
        {
            this.indexTypeRoom = indexTypeRoom;
            this.indexInMatrixI = indexInMatrixI;
            this.indexInMatrixJ = indexInMatrixJ;
            isNull = false;
        }

        /// <summary>
        /// Установить наличие дверей
        /// </summary>
        public void SetPresenceDoors(RoomController roomController)
        {
            isDoorT = roomController.DoorT != null;
            isDoorR = roomController.DoorR != null;
            isDoorB = roomController.DoorB != null;
            isDoorL = roomController.DoorL != null;
        }

        /// <summary>
        /// Установка флага: комната скраю или нет
        /// </summary>
        public void SetIsLastRoom(bool isLastRoom)
        {
            this.isLastRoom = isLastRoom;
        }
    }
}
