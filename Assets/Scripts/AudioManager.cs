using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;

    public bool isMusic = false;

    public bool isLooping;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] sounds;

    private float soundVolume;
    private float musicVolume;

    private void Awake()
    {
        soundVolume = 1;
        musicVolume = 1;

        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        //DontDestroyOnLoad(gameObject);

        ActualiseSounds();
    }

    private void Start()
    {

    }

    public void StartMusic()
    {
        PlaySound("startMusic");
        Invoke("PlayMusic", 3.7f);
    }

    public void PlaySound(string soundname)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundname);
        s.source.Play();

        if (s.source == null)
        {
            Debug.Log(soundname);
        }
    }

    public void StopSound(string soundname)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundname);
        s.source.Pause();
    }

    public void StopAllSound()
    {
        foreach (Sound s in sounds)
        {
            s.source.Pause();
        }
    }

    public void PauseSound(string soundname)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundname);
        s.source.Pause();
    }

    public void UnpauseSound(string soundname)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundname);
        s.source.Pause();
    }

    private void PlayMusic()
    {
        PlaySound("loopMusic");
    }

    public void StopMusic()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "startMusic");
        s.source.Stop();
        Sound sm = Array.Find(sounds, sound => sound.name == "loopMusic");
        sm.source.Stop();
    }

    public bool IsPlaying(string soundname)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundname);
        return s.source.isPlaying;
    }

    public void SetSoundVolume(float newVolume)
    {
        soundVolume = newVolume;
        ActualiseSounds();
    }

    public void SetMusicVolume(float newVolume)
    {
        musicVolume = newVolume;
        ActualiseSounds();
    }

    public void ActualiseSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            if (s.isMusic == true)
            {
                s.source.volume = s.volume * musicVolume;
            }
            else
            {
                s.source.volume = s.volume * soundVolume;
            }

            s.source.pitch = s.pitch;
            s.source.loop = s.isLooping;
        }
    }
}