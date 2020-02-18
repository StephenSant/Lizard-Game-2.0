using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public Track[] tracks;
    public float musicFadeTime = 1;


    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.playOnAwake = false;
            s.source.pitch = s.pitch;
        }

        foreach (Track t in tracks)
        {
            t.source = gameObject.AddComponent<AudioSource>();
            t.source.clip = t.clip;

            t.source.loop = t.loop;
            t.source.volume = t.volume;
            t.source.pitch = t.pitch;
        }
    }

    public void UpdateSound(bool soundIsOn)
    {
        if (soundIsOn)
        {
            foreach (Sound s in sounds)
            {
                s.source.volume = s.volume;
            }
        }
        else
        {
            foreach (Sound s in sounds)
            {

                s.source.volume = 0;
            }
        }
    }

    public void UpdateMusic(bool musicIsOn)
    {
        if (musicIsOn)
        {
            foreach (Track t in tracks)
            {
                t.source.volume = t.volume;
            }
        }
        else
        {
            foreach (Track t in tracks)
            {

                t.source.volume = 0;
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

    public void PlayTrack(string audioName)
    {
        Track t = Array.Find(tracks, track => track.name == audioName);
        if (t == null)
        {
            Debug.LogError("Could not find music track will name - " + audioName);
            return;
        }
        t.source.Play();
    }

    public void FadeInMusic(string audioName)//Causes crash
    {
        Track t = Array.Find(tracks, track => track.name == audioName);
        if (!t.fading)
        {
            while (t.source.volume >= t.volume)
            {
                t.fading = true;
                t.source.volume += Time.deltaTime * musicFadeTime;
            }
            if (t.source.volume >= t.volume)
            {
                t.fading = false;
            }
        }

    }
    public void FadeOutMusic(string audioName)//Causes crash
    {

        Track t = Array.Find(tracks, track => track.name == audioName);
        if (!t.fading)
        {
            while (t.source.volume <= 0)
            {
                t.fading = true;
                t.source.volume -= Time.deltaTime * musicFadeTime;
            }
            if (t.source.volume <= 0)
            {
                t.fading = false;
            }
        }
    }
}
[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1;
    [Range(-3f, 3f)]
    public float pitch = 1;
    [HideInInspector]
    public AudioSource source;
}
[Serializable]
public class Track
{
    public string name;
    public AudioClip clip;
    public bool loop = true;
    [Range(0f, 1f)]
    public float volume = 1;
    [Range(-3f, 3f)]
    public float pitch = 1;
    [HideInInspector]
    public AudioSource source;
    [HideInInspector]
    public bool fading = false;
}