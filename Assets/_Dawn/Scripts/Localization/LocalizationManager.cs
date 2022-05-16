using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    private string currentLanguage;
    private Dictionary<string, string> localizedText;
    public static bool isReady = false;
    
    // делегат события
    public delegate void ChangeLangText();
    public event ChangeLangText OnLanguageChanged;

    private void Awake()
    {
        // --- определяем язык системы: ---
        // проверяем PlayerPrefs, если ключа нет, то смотрим по системнаму языку
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
            {
                PlayerPrefs.SetString("Language", "ru_RU");
            }
            //else if (Application.systemLanguage == SystemLanguage.Afrikaans)
            //{
            //    // пример добавления зыков
            //}
            else // дефолт: английский
            {
                PlayerPrefs.SetString("Language", "en_US");
            }
        }
        currentLanguage = PlayerPrefs.GetString("Language");

        LoadLocalizedText(currentLanguage);
    }

    // главный метод получения локализации
    public void LoadLocalizedText(string langName)
    {
        string path = Application.streamingAssetsPath + "/Languages/" + langName + ".json"; // загрузка json со структурой перевода все текстов

        if (!File.Exists(path))
        {
            Debug.Log(path + " не найден");
            return;
        }

        string dataAsJson;

        // в зависимости от платформы считывания данных в формат string
#if UNITY_ANDROID && !UNITY_EDITOR
        WWW reader = new WWW(path);
        while (!reader.isDone) { }

        dataAsJson = reader.text;
#else
        dataAsJson = File.ReadAllText(path);
#endif
        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    WWW reader = new WWW(path);
        //    while (!reader.isDone) { }

        //    dataAsJson = reader.text;
        //}
        //else
        //{
        //    dataAsJson = File.ReadAllText(path);
        //}



        // перевод по нашему классу-шаблону ч-з JsonUtility
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        // создаем словарь
        localizedText = new Dictionary<string, string>();
        for (int i = 0; i < loadedData.items.Length; i++)
        {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }

        PlayerPrefs.SetString("Language", langName);    // устанавливаем текущий язык
        currentLanguage = PlayerPrefs.GetString("Language");    // не забываем изменить
        isReady = true; // готов ли быть использован словарь конкретного языка

        OnLanguageChanged?.Invoke();    // вызов всех подписанных методов
    }

    // узнаем значение по ключу в текущем языке
    public string GetLocalizedValue(string key)
    {
        if (localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }
        else
        {
            throw new Exception("Localized text with key \"" + key + "\" not found");
        }
    }

    // доступ к текущему языку (ч-з св-во)
    public string CurrentLanguage
    {
        get
        {
            return currentLanguage;
        }
        set
        {
            PlayerPrefs.SetString("Language", value);
            currentLanguage = PlayerPrefs.GetString("Language");
            //LoadLocalizedText(currentLanguage);
        }
    }

    // вызов смены языка
    public void OnButtonClickSwitchLanguage(string nameFileJSON)
    {
        LoadLocalizedText(nameFileJSON);
    }
}
