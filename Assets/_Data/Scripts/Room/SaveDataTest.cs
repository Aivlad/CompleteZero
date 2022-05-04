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
        // �������� ����������
        if (!Directory.Exists(Application.persistentDataPath + "/Save"))
        {
            Directory.CreateDirectory((Application.persistentDataPath + "/Save"));
        }

        //// ������������ ��������
        //RoomController.RoomControllerSave[] saves = new RoomController.RoomControllerSave[souceSave.Length];
        //for (int i = 0; i < saves.Length; i++)
        //{
        //    saves[i] = souceSave[i].GetDataSave();
        //}
        //GlobalSave gs = new GlobalSave(saves);

        //// ����������
        //File.WriteAllText(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json", JsonUtility.ToJson(gs));

        Debug.Log("���������� ���������");
    }

    public void LoadGame()
    {
        if (!File.Exists(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json"))
        {
            Debug.Log("���������� �� �������");
            return;
        }

        //// ��������
        //save = JsonUtility.FromJson<SaveGeneratedLevel>(File.ReadAllText(Application.persistentDataPath + "/Save" + "/" + nameSaveFile + ".json"));

        //// �������������� ��������
        //spawnRoomController.LevelRebuilding(save.GetTransformedMatrixArrangement());

        Debug.Log("�������� ���������");
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
