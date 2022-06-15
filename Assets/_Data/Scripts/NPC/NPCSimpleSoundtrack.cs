using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSimpleSoundtrack : MonoBehaviour
{
    public AudioClip soundtrack;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //PlaySound();
    }

    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.clip = soundtrack;
            audioSource.loop = true;
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
