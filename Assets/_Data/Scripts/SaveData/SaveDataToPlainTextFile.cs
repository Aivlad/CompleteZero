using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class SaveDataToPlainTextFile : MonoBehaviour
{
    private string path = Application.streamingAssetsPath + "/Balance/"; // путь к файлу
    private string nameFileForRooms; // название файла 
    private string nameFileForOther; // название файла 

    private void Start()
    {
        // формируем имя файла
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
        // функция сохранения
        StreamWriter sw = new StreamWriter(path + nameFileForRooms); // создаём файл
        sw.Close(); // закрываем файл
        
        sw = new StreamWriter(path + nameFileForOther); // создаём файл
        sw.Close(); // закрываем файл
        
        Debug.Log("Баланс файлы созданы");
    }

    public void RoomSaveText(string text)
    { 
        // функция сохранения
        StreamWriter sw = new StreamWriter(path + nameFileForRooms, true); // создаём файл
        sw.WriteLine(text); // записываем в файл строку
        sw.Close(); // закрываем 
    }

    public void OtherSaveText(string text)
    {
        // функция сохранения
        StreamWriter sw = new StreamWriter(path + nameFileForOther, true); // создаём файл
        sw.WriteLine(text); // записываем в файл строку
        sw.Close(); // закрываем 
    }

}
