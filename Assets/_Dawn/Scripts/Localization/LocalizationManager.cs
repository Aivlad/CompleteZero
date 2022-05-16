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
    
    // ������� �������
    public delegate void ChangeLangText();
    public event ChangeLangText OnLanguageChanged;

    private void Awake()
    {
        // --- ���������� ���� �������: ---
        // ��������� PlayerPrefs, ���� ����� ���, �� ������� �� ���������� �����
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
            {
                PlayerPrefs.SetString("Language", "ru_RU");
            }
            //else if (Application.systemLanguage == SystemLanguage.Afrikaans)
            //{
            //    // ������ ���������� �����
            //}
            else // ������: ����������
            {
                PlayerPrefs.SetString("Language", "en_US");
            }
        }
        currentLanguage = PlayerPrefs.GetString("Language");

        LoadLocalizedText(currentLanguage);
    }

    // ������� ����� ��������� �����������
    public void LoadLocalizedText(string langName)
    {
        string path = Application.streamingAssetsPath + "/Languages/" + langName + ".json"; // �������� json �� ���������� �������� ��� �������

        if (!File.Exists(path))
        {
            Debug.Log(path + " �� ������");
            return;
        }

        string dataAsJson;

        // � ����������� �� ��������� ���������� ������ � ������ string
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



        // ������� �� ������ ������-������� �-� JsonUtility
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        // ������� �������
        localizedText = new Dictionary<string, string>();
        for (int i = 0; i < loadedData.items.Length; i++)
        {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }

        PlayerPrefs.SetString("Language", langName);    // ������������� ������� ����
        currentLanguage = PlayerPrefs.GetString("Language");    // �� �������� ��������
        isReady = true; // ����� �� ���� ����������� ������� ����������� �����

        OnLanguageChanged?.Invoke();    // ����� ���� ����������� �������
    }

    // ������ �������� �� ����� � ������� �����
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

    // ������ � �������� ����� (�-� ��-��)
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

    // ����� ����� �����
    public void OnButtonClickSwitchLanguage(string nameFileJSON)
    {
        LoadLocalizedText(nameFileJSON);
    }
}
