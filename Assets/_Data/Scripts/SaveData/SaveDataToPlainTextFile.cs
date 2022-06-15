using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class SaveDataToPlainTextFile : MonoBehaviour
{
    private string path = Application.streamingAssetsPath + "/Balance/"; // ���� � �����
    private string nameFileForRooms; // �������� ����� 

    private void Start()
    {
        // �������� ����������
        if (!Directory.Exists(Application.streamingAssetsPath + "/Balance"))
        {
            Directory.CreateDirectory((Application.streamingAssetsPath + "/Balance"));
        }

        // ��������� ��� �����
        nameFileForRooms = "Room"
            + "_" + SceneManager.GetActiveScene().name
            + "_" + DateTime.Now.Year
            + "_" + DateTime.Now.Month
            + "_" + DateTime.Now.Day
            + "__" + DateTime.Now.Hour
            + "_" + DateTime.Now.Minute
            + "_" + DateTime.Now.Second
            + ".txt";
        CreateFile();
    }

    private void CreateFile()
    { 
        // ������� ����������
        StreamWriter sw = new StreamWriter(path + nameFileForRooms); // ������ ����
        sw.Close(); // ��������� ����
        
        Debug.Log("������ ����� �������");
    }

    public void RoomSaveText(string text)
    { 
        // ������� ����������
        StreamWriter sw = new StreamWriter(path + nameFileForRooms, true); // ������ ����
        sw.WriteLine(text); // ���������� � ���� ������
        sw.Close(); // ��������� 
    }
}
