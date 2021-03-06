﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] clips;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    [YarnCommand("playMusic")]
    public void PlaySong(string title)
    {
        int index = 0;
        switch (title)
        {
            case "theme": index = 0;
                break;
            case "bossBattle": index = 1;
                break;
            case "silly": index = 2;
                break;
            case "battle": index = 3;
                break;
        }

        if(source.clip != clips[index])
        {
            source.clip = clips[index];
            source.Play();
        }

    }
}
