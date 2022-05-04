using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataTest : MonoBehaviour
{
    public string nameSaveFile;

    public RoomController[] souceSave;

    public void SaveGame()
    {
        // проверка директории
        if (!Directory.Exists(Application.persistentDataPath + "/Save"))
        {
            Directory.CreateDirectory((Application.persistentDataPath + "/Save"));
        }

        //// присваивание значений
        //RoomController.RoomControllerSave[] saves = new RoomController.RoomControllerSave[souceSave.Length];
        //for (int i = 0; i < saves.Length; i++)
        //{
        //    saves[i] = souceSave[i].GetDataSave();
        //}
        //GlobalSave gs = new GlobalSave(saves);

        //// сохранение
        //File.WriteAllText(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json", JsonUtility.ToJson(gs));

        Debug.Log("Сохранение выполнено");
    }

    public void LoadGame()
    {
        if (!File.Exists(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json"))
        {
            Debug.Log("Сохранение не найдено");
            return;
        }

        //// загрузка
        //save = JsonUtility.FromJson<SaveGeneratedLevel>(File.ReadAllText(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json"));

        //// восстановление значений
        //spawnRoomController.LevelRebuilding(save.GetTransformedMatrixArrangement());

        Debug.Log("Загрузка выполнена");
    }


    public class GlobalSave
    {
        public RoomController.RoomControllerSave[] saves;

        public GlobalSave(RoomController.RoomControllerSave[] saves)
        {
            this.saves = saves;
        }
    }
}
