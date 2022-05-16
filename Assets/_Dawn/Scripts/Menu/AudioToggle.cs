using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{  
    public Image imageSource;
    public Sprite soundOn;
    public Sprite soundOff;

    private void Start()
    {
        // если сейва нет, то запускаем музыку и делаемсейв
        if (!PlayerPrefs.HasKey(KeysPlayerPrefs.SOUND_KEY_PLAYER_PREFS))
        {
            AudioListener.pause = false;
            PlayerPrefs.SetInt(KeysPlayerPrefs.SOUND_KEY_PLAYER_PREFS, AudioListener.pause ? 1 : 0);
        }

        // определяем сейв
        AudioListener.pause = PlayerPrefs.GetInt(KeysPlayerPrefs.SOUND_KEY_PLAYER_PREFS) == 1;

        IconChange();
    }

    // вкл\выкл зввук
    public void ActivatedSound()
    {        
        AudioListener.pause = !AudioListener.pause; // меняем состояние
        PlayerPrefs.SetInt(KeysPlayerPrefs.SOUND_KEY_PLAYER_PREFS, AudioListener.pause ? 1 : 0); //делаем сейв
        IconChange();
    }

    // смена изображения (иконки)
    private void IconChange()
    {
        if (imageSource == null) // ситуация: кнопка без Image
            return;

        if (AudioListener.pause)
            imageSource.sprite = soundOff;
        else
            imageSource.sprite = soundOn;
    }
    /*
     AudioListener.pause = true (1) -> пауза в звуке, т.е. он выключен     
     */
}
