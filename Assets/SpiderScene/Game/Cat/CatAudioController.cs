using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAudioController : MonoBehaviour
{
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void playAudio()
    {
        audioSource.Play();
    }
}
