using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : ButtonScript {

    public AudioClip stimuliSound;
    private AudioSource audioSource;  

    public void Start() {
        // ButtonScipt Start method
        base.Start();

        //AudioButton Start method
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = stimuliSound;
    }

	public override void Begin()
    {
        audioSource.Play();
        timer.StartTimer();
        active = true;
    }
}
