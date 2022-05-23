using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SaveDataToPlainTextFile : MonoBehaviour
{
    public string path = Application.streamingAssetsPath + "/Balance/"; // ���� � �����
    public string nameFile = "test.txt"; // �������� ����� 

    public void OnSave(string text)
    { // ������� ����������
        StreamWriter sw = new StreamWriter(path + nameFile); // ������ ����
        sw.WriteLine(text); // ���������� � ���� ������
        sw.Close(); // ��������� ����
        Debug.Log("OK");
    }

    public void OnSaveAppend(string text)
    { // ������� ����������
        StreamWriter sw = new StreamWriter(path + nameFile, true); // ������ ����
        sw.WriteLine(text); // ���������� � ���� ������
        sw.Close(); // ��������� ����
        Debug.Log("OK");
    }

}
