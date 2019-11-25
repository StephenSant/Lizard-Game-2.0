using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.playOnAwake = false;
        }
    }

    public void UpdateSound(bool soundIsOn)
    {
        if (soundIsOn)
        {
            foreach (Sound s in sounds)
            {
                s.source.volume = 1;
                s.volume = 1;
            }
        }
        else
        {
            foreach (Sound s in sounds)
            {
                s.source.volume = 0;
                s.volume = 0;
            }
        }

    }

    public void PlaySound(string audioName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == audioName);
        if (s == null)
        {
            Debug.LogError("Could not find sound will name - " + audioName);
            return;
        }
        s.source.Play();
    }
}
[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1;
    [HideInInspector]
    public AudioSource source;
}