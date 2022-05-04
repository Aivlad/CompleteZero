using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ResolutionSettings : MonoBehaviour
{
    public Dropdown dropdown;
    //public Toggle toggle;
    private Resolution[] resolutions;

    private const string KEY_SAVE_RESOLUTION = "RESOLUTION_KEY_SAVE";
    private const string KEY_SAVE_WINDOWS_MODE = "WINDOWS_MODE_KEY_SAVE";

    private void Start()
    {
        // полноэкранный режим
        if (PlayerPrefs.HasKey(KEY_SAVE_WINDOWS_MODE))
        {
            int save_value = PlayerPrefs.GetInt(KEY_SAVE_WINDOWS_MODE);
            Screen.fullScreen = save_value != 0;
        }
        else
        {
            Screen.fullScreen = true;
        }
        //toggle.isOn = !Screen.fullScreen;


        // заполнение
        resolutions = Screen.resolutions
            .Distinct() // убираем множественные повторы
            .ToArray(); // после distinct получаем енумератор, его обратно в массив
        string[] strResolutions = new string[resolutions.Length];
        for (int i = 0; i < resolutions.Length; i++)
        {
            strResolutions[i] = resolutions[i].ToString();
            //strResolutions[i] = resolutions[i].width.ToString() + " x " + resolutions[i].height.ToString();
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(strResolutions.ToList());

        if (PlayerPrefs.HasKey(KEY_SAVE_RESOLUTION))
        {
            var save_value = PlayerPrefs.GetInt(KEY_SAVE_RESOLUTION);
            dropdown.value = save_value;
            Screen.SetResolution(resolutions[save_value].width, resolutions[save_value].height, Screen.fullScreen);
        }
        else
        {
            // устанавливаем макс. разрешение
            dropdown.value = resolutions.Length - 1;
            Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, Screen.fullScreen);
        }
    }

    public void SetRes()
    {
        Screen.SetResolution(resolutions[dropdown.value].width, resolutions[dropdown.value].height, Screen.fullScreen);
        PlayerPrefs.SetInt(KEY_SAVE_RESOLUTION, dropdown.value);
    }

    public void ScreenMode()
    {
        //Screen.fullScreen = !toggle.isOn;
        PlayerPrefs.SetInt(KEY_SAVE_WINDOWS_MODE, Screen.fullScreen ? 1 : 0);
    }
}
