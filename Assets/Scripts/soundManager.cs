using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class soundManager : MonoBehaviour
{
    public soundClass[] sounds;

    private void Awake()
    {
        foreach (soundClass s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }
    }

    public void Play (string name)
    {
        soundClass s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void Stop(string name)
    {
        soundClass s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void Pause(string name)
    {
        soundClass s = Array.Find(sounds, sound => sound.name == name);
        s.source.Pause();
    }

    public void Resume(string name)
    {
        soundClass s = Array.Find(sounds, sound => sound.name == name);
        s.source.UnPause();
    }
}
