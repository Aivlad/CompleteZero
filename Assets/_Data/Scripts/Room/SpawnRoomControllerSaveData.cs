using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpawnRoomControllerSaveData : MonoBehaviour
{
    public string nameSaveFile;

    private SpawnRoomController spawnRoomController;
    private SaveGeneratedLevel save = new SaveGeneratedLevel();

    private void Start()
    {
        spawnRoomController = GetComponent<SpawnRoomController>();
    }

    public void SaveLevel()
    {
        // проверка директории
        if (!Directory.Exists(Application.persistentDataPath + "/Save"))
        {
            Directory.CreateDirectory((Application.persistentDataPath + "/Save"));
        }

        // присваивание значений
        RoomController[,] spawnedRooms = spawnRoomController.GetSpawnedRooms(); // источник дл€ сохранени€
        RoomController.RoomControllerSave[] savesData = new RoomController.RoomControllerSave[spawnedRooms.Length];
        int k = 0;
        for (int i = 0; i < spawnedRooms.GetLength(0); i++)
        {
            for (int j = 0; j < spawnedRooms.GetLength(1); j++)
            {
                savesData[k] = spawnedRooms[i, j]?.GetDataSave();
                k++;
            }
        }
        save.SetData(savesData);

        //сохранение
        File.WriteAllText(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json", JsonUtility.ToJson(save));

        Debug.Log("—охранение выполнено");
    }

    public void LoadLevel()
    {
        if (!File.Exists(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json"))
        {
            Debug.Log("—охранение не найдено");
            return;
        }

        // загрузка
        save = JsonUtility.FromJson<SaveGeneratedLevel>(File.ReadAllText(Application.persistentDataPath + "/Save" + "/"+ nameSaveFile +".json"));

        // восстановление значений
        spawnRoomController.LevelRebuilding(save.GetGeneratedRooms());

        Debug.Log("«агрузка выполнена");
    }


    /// <summary>
    ///  ласс сохранени€ дл€ SpawnRoomController
    /// </summary>
    public class SaveGeneratedLevel
    {
        public RoomController.RoomControllerSave[] generatedRooms;

        public void SetData(RoomController.RoomControllerSave[] generatedRooms)
        {
            this.generatedRooms = generatedRooms;
        }

        public RoomController.RoomControllerSave[] GetGeneratedRooms()
        {
            return generatedRooms;
        }
    }
}
