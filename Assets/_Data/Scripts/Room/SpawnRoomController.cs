using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SpawnRoomController : MonoBehaviour
{
    [Header("Room generation")]
    [Range(2, 50)]
    public int countGeneratedRooms;                 // сколько комнат нужно заспавнить
    public RoomController[] typesRooms;             // доступные типы комнат для спавна
    public int roomLength;                          // длина комнаты
    public int roomHeight;                          // высота комнаты
    private RoomController[,] spawnedRooms;         // матрица заспавленных комнат

    /// <summary>
    /// Генерация уровня с нуля
    /// </summary>
    public void LevelGeneration()
    {
        // создание и расположение стартовой комнаты
        RoomController startingRoom = Instantiate(typesRooms[Random.Range(0, typesRooms.Length)], Vector3.zero, Quaternion.identity);
        spawnedRooms = new RoomController[11, 11];  // работаем с матрицей 11х11, где 11 - СЧ
        spawnedRooms[5, 5] = startingRoom;  // помещаем стартовую комнату в центр

        // генерация еще countGeneratedRooms - 1 комнат (-1 т.к. startingRoom уже есть)
        for (int i = 0; i < countGeneratedRooms - 1; i++)
        {
            CreateOneRoom();
        }

        // закрываем открытые проходы дверьми, превращаем не открытые двери в стены (визуально)
        foreach (RoomController room in spawnedRooms)
        {
            if (room == null) continue;
            room.ClosePassagesAndOpenPlugs();
        }

        Debug.Log("Уровень сгенерирован");
    }

    /// <summary>
    /// Создание одной комнаты
    /// </summary>
    private void CreateOneRoom()
    {
        // проверка: где можно заспавнить комнату (как соседа)

        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();   // места куда можно поставить комнату
        // проходим по всем клеткам spawnedRooms
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

        // создаем непосредственно комнату
        RoomController newRoom = Instantiate(typesRooms[Random.Range(0, typesRooms.Length)]);

        int limit = 1000;
        while (limit-- > 0)
        {
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            if (ConnectToSomething(newRoom, position))
            {
                newRoom.transform.position = new Vector3((position.x - 5) * roomLength, (position.y - 5) * roomHeight, 0);   //5 - положение startingRoom
                spawnedRooms[position.x, position.y] = newRoom;
                return;
            }
        }

        Destroy(newRoom);
    }

    /// <summary>
    /// Создание прохода между room (на позиции p) и одной из доступных соседних комнат
    /// </summary>
    /// <returns>True - все прошло удачно</returns>
    private bool ConnectToSomething(RoomController room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        // определение соседей с допустимым проходом
        List<Vector2Int> neighbours = new List<Vector2Int>();   // в этом списке все варианты куда точно сможем подсоеденить эту комнату
        if (room.DoorT != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorB != null) neighbours.Add(Vector2Int.up);
        if (room.DoorB != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorT != null) neighbours.Add(Vector2Int.down);
        if (room.DoorR != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorL != null) neighbours.Add(Vector2Int.right);
        if (room.DoorL != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorR != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        RoomController selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];

        //выключаем двери
        if (selectedDirection == Vector2Int.up)
        {
            room.DoorT.SetActive(false);
            selectedRoom.DoorB.SetActive(false);

            // добавляем дверь соседа как вход в текущую комнату
            room.neighborExitDoor = selectedRoom.DoorB;
        }
        else if (selectedDirection == Vector2Int.down)
        {
            room.DoorB.SetActive(false);
            selectedRoom.DoorT.SetActive(false);

            // добавляем дверь соседа как вход в текущую комнату
            room.neighborExitDoor = selectedRoom.DoorT;
        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.DoorR.SetActive(false);
            selectedRoom.DoorL.SetActive(false);

            // добавляем дверь соседа как вход в текущую комнату
            room.neighborExitDoor = selectedRoom.DoorL;
        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.DoorL.SetActive(false);
            selectedRoom.DoorR.SetActive(false);

            // добавляем дверь соседа как вход в текущую комнату
            room.neighborExitDoor = selectedRoom.DoorR;
        }

        // помечаем, что из текущей комнады есть проход, она не крайняя (имеет неск. выходов)
        // + заспавненая комната - теперь крайняя
        room.neighborExitRoom = selectedRoom;

        return true;
    }


    /// <summary>
    /// Функциональный метод: очистка всех сгенерированных комнат (используется только для удобства при отладке)
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
        Debug.Log("Уровень очищен");
    }
}
