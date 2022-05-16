using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ActivationCreditsPanel : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private void OnEnable()
    {
        videoPlayer.Play();
    }


    private void OnDisable()
    {
        videoPlayer.Stop();
    }
}
