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
        // ���� ����� ���, �� ��������� ������ � ����������
        if (!PlayerPrefs.HasKey(KeysPlayerPrefs.SOUND_KEY_PLAYER_PREFS))
        {
            AudioListener.pause = false;
            PlayerPrefs.SetInt(KeysPlayerPrefs.SOUND_KEY_PLAYER_PREFS, AudioListener.pause ? 1 : 0);
        }

        // ���������� ����
        AudioListener.pause = PlayerPrefs.GetInt(KeysPlayerPrefs.SOUND_KEY_PLAYER_PREFS) == 1;

        IconChange();
    }

    // ���\���� �����
    public void ActivatedSound()
    {        
        AudioListener.pause = !AudioListener.pause; // ������ ���������
        PlayerPrefs.SetInt(KeysPlayerPrefs.SOUND_KEY_PLAYER_PREFS, AudioListener.pause ? 1 : 0); //������ ����
        IconChange();
    }

    // ����� ����������� (������)
    private void IconChange()
    {
        if (imageSource == null) // ��������: ������ ��� Image
            return;

        if (AudioListener.pause)
            imageSource.sprite = soundOff;
        else
            imageSource.sprite = soundOn;
    }
    /*
     AudioListener.pause = true (1) -> ����� � �����, �.�. �� ��������     
     */
}
