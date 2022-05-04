using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpawnRoomControllerSaveData : MonoBehaviour
{
    public string nameSaveFile;

    private SpawnRoomController spawnRoomController;
    private SaveGeneratedLevel save = new SaveGeneratedLevel(11);
    //private RoomController[,] spawnedRooms;

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
        RoomController[,] spawnedRooms = spawnRoomController.GetSpawnedRooms();
        int[] transformedMatrixArrangement = new int[spawnedRooms.Length];
        int k = 0;
        for (int i = 0; i < spawnedRooms.GetLength(0); i++)
        {
            for (int j = 0; j < spawnedRooms.GetLength(1); j++)
            {
                if (spawnedRooms[i, j] != null)
                {
                    transformedMatrixArrangement[k] = spawnedRooms[i, j].indexInList;
                }
                else
                {
                    transformedMatrixArrangement[k] = -1;
                }
                k++;
            }
        }
        save.SetData(transformedMatrixArrangement);

        // сохранение
        File.WriteAllText(Application.persistentDataPath + "/Save" + "/"+ nameSaveFile +".json", JsonUtility.ToJson(save));

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
        save = JsonUtility.FromJson<SaveGeneratedLevel>(File.ReadAllText(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json"));

        // восстановление значений
        spawnRoomController.LevelRebuilding(save.GetTransformedMatrixArrangement());

        Debug.Log("«агрузка выполнена");
    }


    /// <summary>
    ///  ласс сохранени€ дл€ SpawnRoomController
    /// </summary>
    public class SaveGeneratedLevel
    {
        /// <summary>
        /// –азмер массивов (беретс€ как spawnedRooms.GetLength(0))
        /// </summary>
        public int size;
        /// <summary>
        /// ѕреобразованное сохранение матрицы (квадратной) spawnedRooms (-1 если null)
        /// </summary>
        public int[] transformedMatrixArrangement;

        // todo: сделать save дл€ отключени€ ненужных дверей (а-л€ save комнаты входа в текущ. комнату)
        // todo: сделать save открытых дверей


        public SaveGeneratedLevel(int size)
        {
            this.size = size;
            transformedMatrixArrangement = new int[size * size];
        }

        public void SetData(int[] transformedMatrixArrangement)
        {
            this.transformedMatrixArrangement = transformedMatrixArrangement;
        }

        public int[] GetTransformedMatrixArrangement()
        {
            return transformedMatrixArrangement;
        }
    }
}
