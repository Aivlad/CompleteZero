using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GraphicSettings : MonoBehaviour
{
    private const string KEY_SAVE = "QUALITY_KEY_SAVE";
    public Dropdown dropdown;

    private void Start()
    {
        // заполнение
        dropdown.ClearOptions();
        dropdown.AddOptions(QualitySettings.names.ToList());

        // текущий уровень графики
        if (PlayerPrefs.HasKey(KEY_SAVE))
        {
            var save_value = PlayerPrefs.GetInt(KEY_SAVE);
            dropdown.value = save_value;
            QualitySettings.SetQualityLevel(save_value);
        }
        else
        {
            dropdown.value = QualitySettings.GetQualityLevel();
        }

    }


    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt(KEY_SAVE, dropdown.value);
    }
}
