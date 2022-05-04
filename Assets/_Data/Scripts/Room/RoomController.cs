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
    [HideInInspector] public GameObject neighborExitDoor = null;    // дверь-переход соседа в эту комнату
    [HideInInspector] public RoomController neighborExitRoom = null;    // сосед, из которого пришли в эту комнату

    [Header("Plugs")]
    public GameObject parentPlugs;
    public GameObject prefabPlug;

    [Header("Metadata")]
    [HideInInspector] public int indexInList; // индекс в списке, где необходимо (например, при генерации для save)

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
                var inst = Instantiate(prefabPlug, door.transform.position, door.transform.rotation);
                inst.transform.parent = parentPlugs.transform;
                Destroy(door);
                continue;
            }
            // неактивы (т.е. проходы) закрываем дверьми
            door.SetActive(true);
        }
    }
}
