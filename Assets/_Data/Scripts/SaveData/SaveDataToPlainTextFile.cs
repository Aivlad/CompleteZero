using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SaveDataToPlainTextFile : MonoBehaviour
{
    public string path = Application.streamingAssetsPath + "/Balance/"; // путь к файлу
    public string nameFile = "test.txt"; // название файла 

    public void OnSave(string text)
    { // функция сохранения
        StreamWriter sw = new StreamWriter(path + nameFile); // создаём файл
        sw.WriteLine(text); // записываем в файл строку
        sw.Close(); // закрываем файл
        Debug.Log("OK");
    }

    public void OnSaveAppend(string text)
    { // функция сохранения
        StreamWriter sw = new StreamWriter(path + nameFile, true); // создаём файл
        sw.WriteLine(text); // записываем в файл строку
        sw.Close(); // закрываем файл
        Debug.Log("OK");
    }

}
