using System.Collections;
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
        }

        source.clip = clips[index];
        source.Play();
    }
}
