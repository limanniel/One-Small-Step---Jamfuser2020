using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

[System.Serializable]
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach(Sound audio in sounds)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;

            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
        }
    }

    public void Play(string name)
    {
        Sound clip = Find(name);
        clip.source.Play();
    }

    private Sound Find(string name)
    {
        Sound clipToPlay = Array.Find(sounds, sound => sound.name == name);
        return clipToPlay;
    }

    public void Play(string name, float volume, float pitch)
    {
        Sound clip = Find(name);
        if (clip != null)
        {
            clip.source.volume = volume;
            clip.source.pitch = pitch;
            clip.source.Play();
        } 
        
    }
}
