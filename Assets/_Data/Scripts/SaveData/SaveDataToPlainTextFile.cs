using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class SaveDataToPlainTextFile : MonoBehaviour
{
    private string path = Application.streamingAssetsPath + "/Balance/"; // ���� � �����
    private string nameFileForRooms; // �������� ����� 
    private string nameFileForOther; // �������� ����� 

    private void Start()
    {
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
        nameFileForOther = "Other"
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
        
        sw = new StreamWriter(path + nameFileForOther); // ������ ����
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

    public void OtherSaveText(string text)
    {
        // ������� ����������
        StreamWriter sw = new StreamWriter(path + nameFileForOther, true); // ������ ����
        sw.WriteLine(text); // ���������� � ���� ������
        sw.Close(); // ��������� 
    }

}
