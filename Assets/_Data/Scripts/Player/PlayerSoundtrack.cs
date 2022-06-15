using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundtrack : MonoBehaviour
{
    public AudioClip soundtrack;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(bool loop = true)
    {
        if (audioSource != null)
        {
            audioSource.clip = soundtrack;
            audioSource.loop = loop;
            audioSource.Play();
        }
    }

    public void StopSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
