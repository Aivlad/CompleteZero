using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
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
    [HideInInspector] public int indexInList; // ������ � ������, ��� ���������� (��������, ��� ��������� ��� save)

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
                var inst = Instantiate(prefabPlug, door.transform.position, door.transform.rotation);
                inst.transform.parent = parentPlugs.transform;
                Destroy(door);
                continue;
            }
            // �������� (�.�. �������) ��������� �������
            door.SetActive(true);
        }
    }
}
