using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayOnGrab : MonoBehaviour
{
    public AudioClip impact;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter()
    {
        audio.PlayOneShot(impact, 0.7F);
    }
}

