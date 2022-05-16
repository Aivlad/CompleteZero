using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    private static readonly string SOUND_KEY_PLAYER_PREFS = "SOUND_KEY_PLAYER_PREFS";

    private Image componentImage;
    public Sprite soundOn;
    public Sprite soundOff;

    private void Start()
    {
        componentImage = GetComponent<Image>();

        // ���� ����� ���, �� ��������� ������ � ����������
        if (!PlayerPrefs.HasKey(SOUND_KEY_PLAYER_PREFS))
        {
            AudioListener.pause = false;
            PlayerPrefs.SetInt(SOUND_KEY_PLAYER_PREFS, AudioListener.pause ? 1 : 0);
        }

        // ���������� ����
        AudioListener.pause = PlayerPrefs.GetInt(SOUND_KEY_PLAYER_PREFS) == 1;

        IconChange();
    }

    // ���\���� �����
    public void ActivatedSound()
    {        
        AudioListener.pause = !AudioListener.pause; // ������ ���������
        PlayerPrefs.SetInt(SOUND_KEY_PLAYER_PREFS, AudioListener.pause ? 1 : 0); //������ ����
        IconChange();
    }

    // ����� ����������� (������)
    private void IconChange()
    {
        if (componentImage == null) // ��������: ������ ��� Image
            return;

        if (AudioListener.pause)
            componentImage.sprite = soundOff;
        else
            componentImage.sprite = soundOn;
    }

}
