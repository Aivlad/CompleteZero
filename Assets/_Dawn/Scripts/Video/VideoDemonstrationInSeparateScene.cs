using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoDemonstrationInSeparateScene : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public KeyCode buttonScip = KeyCode.Space;
    [Space]
    public MenuController menuController;
    public string nameLoadingScene;
    [Space]
    public bool isGetNameFromSave = false;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd;

        // ��������� ����
        bool isPauseSound;
        if (!PlayerPrefs.HasKey(KeysPlayerPrefs.SOUND_KEY_PLAYER_PREFS))
        {
            // � ����� ������ �� ��������� �.�. � 1� ����� ��� �����������
            isPauseSound = false;
        }
        else
        {
            isPauseSound = AudioListener.pause = PlayerPrefs.GetInt(KeysPlayerPrefs.SOUND_KEY_PLAYER_PREFS) == 1;
        }
        videoPlayer.SetDirectAudioMute(0, isPauseSound);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(buttonScip))
        {
            videoPlayer.frame = (long)videoPlayer.frameCount;
        }
    }

    //������ ��� ���, ������� ����� �����������, ����� ����� ����������
    void OnVideoEnd(VideoPlayer causedVideoPlayer)
    {
        if (!isGetNameFromSave)
            menuController.StartScene(nameLoadingScene);
        else
        {
            if (PlayerPrefs.HasKey(KeysPlayerPrefs.SCENE_NAME_AFTER_VIDEO_DISPLAY))
            {
                menuController.StartScene(PlayerPrefs.GetString(KeysPlayerPrefs.SCENE_NAME_AFTER_VIDEO_DISPLAY));
            }
            else
            {
                Debug.LogError("���������� �������� ��������");
            }
        }    
    }

}
