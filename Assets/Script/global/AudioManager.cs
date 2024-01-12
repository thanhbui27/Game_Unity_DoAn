using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] musicSound, sfxSound;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Scene m_Scene = SceneManager.GetActiveScene();
        if (m_Scene.name.Equals("Start Scenes"))
        {
            PlayMusic("start");
        }
        else
        {
            PlayMusic("map");
        }

    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSound, m => m.name.Equals(name));
        if (s != null)
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
        else
        {
            Debug.Log("sound not found");
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSound, m => m.name.Equals(name));
        if (s != null)
        {
            sfxSource.PlayOneShot(s.clip);

        }
        else
        {
            Debug.Log("sfx sound not found");
        }
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

}

