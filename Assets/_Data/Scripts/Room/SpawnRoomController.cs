using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SpawnRoomController : MonoBehaviour
{
    [Header("Room generation")]
    [Range(2, 50)]
    public int countGeneratedRooms;                 // ������� ������ ����� ����������
    public RoomController[] typesRooms;             // ��������� ���� ������ ��� ������
    public int roomLength;                          // ����� �������
    public int roomHeight;                          // ������ �������
    private RoomController[,] spawnedRooms;         // ������� ������������ ������

    /// <summary>
    /// ��������� ������ � ����
    /// </summary>
    public void LevelGeneration()
    {
        // �������� � ������������ ��������� �������
        RoomController startingRoom = Instantiate(typesRooms[Random.Range(0, typesRooms.Length)], Vector3.zero, Quaternion.identity);
        spawnedRooms = new RoomController[11, 11];  // �������� � �������� 11�11, ��� 11 - ��
        spawnedRooms[5, 5] = startingRoom;  // �������� ��������� ������� � �����

        // ��������� ��� countGeneratedRooms - 1 ������ (-1 �.�. startingRoom ��� ����)
        for (int i = 0; i < countGeneratedRooms - 1; i++)
        {
            CreateOneRoom();
        }

        // ��������� �������� ������� �������, ���������� �� �������� ����� � ����� (���������)
        foreach (RoomController room in spawnedRooms)
        {
            if (room == null) continue;
            room.ClosePassagesAndOpenPlugs();
        }

        Debug.Log("������� ������������");
    }

    /// <summary>
    /// �������� ����� �������
    /// </summary>
    private void CreateOneRoom()
    {
        // ��������: ��� ����� ���������� ������� (��� ������)

        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();   // ����� ���� ����� ��������� �������
        // �������� �� ���� ������� spawnedRooms
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;

                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }

        // ������� ��������������� �������
        RoomController newRoom = Instantiate(typesRooms[Random.Range(0, typesRooms.Length)]);

        int limit = 1000;
        while (limit-- > 0)
        {
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            if (ConnectToSomething(newRoom, position))
            {
                newRoom.transform.position = new Vector3((position.x - 5) * roomLength, (position.y - 5) * roomHeight, 0);   //5 - ��������� startingRoom
                spawnedRooms[position.x, position.y] = newRoom;
                return;
            }
        }

        Destroy(newRoom);
    }

    /// <summary>
    /// �������� ������� ����� room (�� ������� p) � ����� �� ��������� �������� ������
    /// </summary>
    /// <returns>True - ��� ������ ������</returns>
    private bool ConnectToSomething(RoomController room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        // ����������� ������� � ���������� ��������
        List<Vector2Int> neighbours = new List<Vector2Int>();   // � ���� ������ ��� �������� ���� ����� ������ ������������ ��� �������
        if (room.DoorT != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorB != null) neighbours.Add(Vector2Int.up);
        if (room.DoorB != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorT != null) neighbours.Add(Vector2Int.down);
        if (room.DoorR != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorL != null) neighbours.Add(Vector2Int.right);
        if (room.DoorL != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorR != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        RoomController selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];

        //��������� �����
        if (selectedDirection == Vector2Int.up)
        {
            room.DoorT.SetActive(false);
            selectedRoom.DoorB.SetActive(false);

            // ��������� ����� ������ ��� ���� � ������� �������
            room.neighborExitDoor = selectedRoom.DoorB;
        }
        else if (selectedDirection == Vector2Int.down)
        {
            room.DoorB.SetActive(false);
            selectedRoom.DoorT.SetActive(false);

            // ��������� ����� ������ ��� ���� � ������� �������
            room.neighborExitDoor = selectedRoom.DoorT;
        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.DoorR.SetActive(false);
            selectedRoom.DoorL.SetActive(false);

            // ��������� ����� ������ ��� ���� � ������� �������
            room.neighborExitDoor = selectedRoom.DoorL;
        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.DoorL.SetActive(false);
            selectedRoom.DoorR.SetActive(false);

            // ��������� ����� ������ ��� ���� � ������� �������
            room.neighborExitDoor = selectedRoom.DoorR;
        }

        // ��������, ��� �� ������� ������� ���� ������, ��� �� ������� (����� ����. �������)
        // + ����������� ������� - ������ �������
        room.neighborExitRoom = selectedRoom;

        return true;
    }


    /// <summary>
    /// �������������� �����: ������� ���� ��������������� ������ (������������ ������ ��� �������� ��� �������)
    /// </summary>
    public void ClearGeneratedLevel()
    {
        for (int i = 0; i < spawnedRooms.GetLength(0); i++)
        {
            for (int j = 0; j < spawnedRooms.GetLength(1); j++)
            {
                if (spawnedRooms[i, j] == null) continue;
                Destroy(spawnedRooms[i, j].gameObject);
                spawnedRooms[i, j] = null;
            }

        }
        Debug.Log("������� ������");
    }
}
