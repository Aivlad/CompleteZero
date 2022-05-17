using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSoundAccompanimentDoor : MonoBehaviour
{
    public AudioClip soundDoorOpen;
    public AudioClip soundDoorClose;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundDoorClose()
    {
        if (audioSource != null)
        {
            audioSource.clip = soundDoorClose;
            audioSource.loop = false;
            audioSource.Play();
        }
    }

    public void PlaySoundDoorOpen()
    {
        if (audioSource != null)
        {
            audioSource.clip = soundDoorOpen;
            audioSource.loop = false;
            audioSource.Play();
        }
    }
}
