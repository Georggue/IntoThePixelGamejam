﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraSound : MonoBehaviour
{
    public UnityAction BeatFired = delegate { };
    public AudioSource Level1Music;
    public AudioSource Level2Music;
    void Start()
    {
      
        //Select the instance of AudioProcessor and pass a reference
        //to this object
        //AudioProcessor processor = FindObjectOfType<AudioProcessor>();
        //processor.onBeat.AddListener(onOnbeatDetected);
        //processor.onSpectrum.AddListener(onSpectrum);
    }

    public void StartMusic(int level)
    {
        switch (level)
        {
            case 1: Level1Music.PlayDelayed(2f);
                break;
            case 2: Level2Music.PlayDelayed(0.9f);
                break;
        }
    }
    public void StopMusic()
    {
       Level1Music.Stop();
       Level2Music.Stop();
    }

    //this event will be called every time a beat is detected.
    //Change the threshold parameter in the inspector
    //to adjust the sensitivity
    void onOnbeatDetected()
    {
        //Debug.Log("Beat!!!");
        BeatFired();
        //todo: spawn new Note on random Line;
    }

    //This event will be called every frame while music is playing
    void onSpectrum(float[] spectrum)
    {
        //The spectrum is logarithmically averaged
        //to 12 bands

        for (int i = 0; i < spectrum.Length; ++i)
        {
            Vector3 start = new Vector3(i, 0, 0);
            Vector3 end = new Vector3(i, spectrum[i], 0);
            Debug.DrawLine(start, end);
        }
    }
}
