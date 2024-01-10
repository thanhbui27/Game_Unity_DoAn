using UnityEngine;
using UnityEngine.UI;

public class UIManagerAudio : MonoBehaviour
{
    public Slider musicVolume, sfxVolume;

    public void Update()
    {
        AudioManager.instance.MusicVolume(musicVolume.value);
        AudioManager.instance.SFXVolume(sfxVolume.value);
    }

    public void ActionMusicVolume()
    {
        AudioManager.instance.MusicVolume(musicVolume.value);
    }
    public void ActionSFXVolume()
    {
        AudioManager.instance.SFXVolume(sfxVolume.value);
    }
}
