﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnGrab2 : MonoBehaviour {

    public AudioClip impact;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
       // audio.PlayOneShot(impact, 0.7F);
    }
    public AudioClip myclip;

    // Use this for initialization
    void OnCollisionEnter()
    {
        this.gameObject.AddComponent<AudioSource>();
        this.GetComponent<AudioSource>().clip = myclip;
        this.GetComponent<AudioSource>().PlayDelayed(2);
    }

}

/*using UnityEngine;
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
}*/

/*   void OnTriggerEnter(Collider c)
{
   Debug.Log(c.name);
}

void OnCollisionEnter(Collision c)
{
   Debug.Log(c.transform.tag);
}*/