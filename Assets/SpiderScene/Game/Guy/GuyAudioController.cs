using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyAudioController : MonoBehaviour
{
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playAudio()
    {
        audioSource.Play();
    }
}
