using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
	private AudioSource audio;
	private Animator anim;
	public AudioClip deathAudio;
	public AudioClip deathClip;
	//public animcontroller

    // Start is called before the first frame update
    void Start()
    {
		audio = GetComponent<AudioSource>();
		anim = GetComponentInChildren<Animator>();

		audio.clip = deathAudio;
		audio.Play();

		Destroy(gameObject, audio.time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
