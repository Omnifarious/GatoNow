using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnGrab : MonoBehaviour {

    // Use this for initialization
    public AudioClip myclip;


	
	// Update is called once per frame
	void OnCollisionEnter (Collision c) {
        if (c.collider.tag.Equals("RightController"))
        {
        this.gameObject.AddComponent<AudioSource>();
        this.GetComponent<AudioSource>().clip = myclip;
        this.GetComponent<AudioSource>().Play();
        }
	}
    
    
}
