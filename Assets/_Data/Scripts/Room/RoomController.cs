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
    [HideInInspector] public GameObject neighborExitDoor = null;    // �����-������� ������ � ��� �������
    [HideInInspector] public RoomController neighborExitRoom = null;    // �����, �� �������� ������ � ��� �������

    [Header("Plugs")]
    public GameObject parentPlugs;
    public GameObject prefabPlug;

    [Header("Metadata")]
    private RoomControllerSave dataSave = new RoomControllerSave();
    private bool isLastRoom = true;

    /// <summary>
    /// ������ ��� �����
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
    /// ������� ������� �������, � ������ ������ �������� �������� ��������
    /// </summary>
    public void ClosePassagesAndOpenPlugs()
    {
        // ��� �������� ��������� ��������� ������
        GameObject[] doors = new GameObject[4];
        doors[0] = DoorT;
        doors[1] = DoorR;
        doors[2] = DoorB;
        doors[3] = DoorL;

        // ��������� ������� �������, ������� �������� � ������ ��������
        foreach (GameObject door in doors)
        {
            // ���������� ���������� � ������ ��������
            if (door.activeInHierarchy)
            {
                CloseDoorWithPlug(door);
                continue;
            }
            // �������� (�.�. �������) ��������� �������
            door.SetActive(true);
        }
    }

    /// <summary>
    /// ���������� ����� � �������� �������� �� �� �����
    /// </summary>
    public void CloseDoorWithPlug(GameObject door)
    {
        var inst = Instantiate(prefabPlug, door.transform.position, door.transform.rotation);
        inst.transform.parent = parentPlugs.transform;
        Destroy(door);
    }

    /// <summary>
    /// ���������� ��������� ������ ���������
    /// </summary>
    public void SetPrimaryData(int indexTypeRoom, int indexInMatrixI, int indexInMatrixJ)
    {
        dataSave.SetPrimaryData(indexTypeRoom, indexInMatrixI, indexInMatrixJ);
    }

    /// <summary>
    /// ���������� ��������� ������ (true - ��������, false - destroy)
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
    /// �������� �� �������� �������
    /// </summary>
    public void SetIsLastRoom(bool isLast = false)
    {
        isLastRoom = isLast;
    }

    /// <summary>
    /// �������� isLastRoom
    /// </summary>
    public bool GetIsLastRoom()
    {
        return isLastRoom;
    }

    /// <summary>
    /// �������� ������ ��� ����������
    /// </summary>
    /// <returns>������ ����������</returns>
    public RoomControllerSave GetDataSave()
    {
        dataSave.SetPresenceDoors(this);
        dataSave.SetIsLastRoom(isLastRoom);
        return dataSave;
    }

    [Serializable]
    public class RoomControllerSave
    {
        public bool isNull; // ��� JSON, ����� ������� �������� ������� �������

        public int indexTypeRoom; // ������ � ������ ����� (�� ����� � ����� ����� ��������)

        public int indexInMatrixI;  // ������ i � ������� ��� ���������  (�� ����� � ����� ����t��)
        public int indexInMatrixJ;  // ������ j � ������� ��� ���������  (�� ����� � ����� ����t��)


        // ������� ������
        public bool isDoorT;
        public bool isDoorR;
        public bool isDoorB;
        public bool isDoorL;

        // ��������� ������� ��� ���
        public bool isLastRoom;

        public RoomControllerSave()
        {
            isNull = true;
        }

        /// <summary>
        /// ���������� ��������� ������
        /// </summary
        public void SetPrimaryData(int indexTypeRoom, int indexInMatrixI, int indexInMatrixJ)
        {
            this.indexTypeRoom = indexTypeRoom;
            this.indexInMatrixI = indexInMatrixI;
            this.indexInMatrixJ = indexInMatrixJ;
            isNull = false;
        }

        /// <summary>
        /// ���������� ������� ������
        /// </summary>
        public void SetPresenceDoors(RoomController roomController)
        {
            isDoorT = roomController.DoorT != null;
            isDoorR = roomController.DoorR != null;
            isDoorB = roomController.DoorB != null;
            isDoorL = roomController.DoorL != null;
        }

        /// <summary>
        /// ��������� �����: ������� ����� ��� ���
        /// </summary>
        public void SetIsLastRoom(bool isLastRoom)
        {
            this.isLastRoom = isLastRoom;
        }
    }
}
