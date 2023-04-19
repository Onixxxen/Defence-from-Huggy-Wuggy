using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsView : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _sfx;
    [SerializeField] private List<AudioSource> _musics;
    [SerializeField] private Slider _sfxSetting;
    [SerializeField] private Slider _musicSetting;

    private float _previuosSFXVolume;
    private float _previuosMusicVolume;

    private void Awake()
    {
        SetSFXVolume();
        SetMusicVolume();

        UpdateSoundVolume();
    }

    public void SetSFXVolume()
    {
        foreach (var sfx in _sfx)
            sfx.volume = _sfxSetting.value;

        UpdateSFXVolume();
    }

    public void SetMusicVolume()
    {
        foreach (var music in _musics)
            music.volume = _musicSetting.value;

        UpdateMisucVolume();
    }

    public void PauseSoundVolume()
    {
        foreach (var sfx in _sfx)
            sfx.volume = 0;

        foreach (var music in _musics)
            music.volume = 0;
    }

    public void BackSoundVolume()
    {
        foreach (var sfx in _sfx)
            sfx.volume = _previuosSFXVolume;

        foreach (var music in _musics)
            music.volume = _previuosMusicVolume;
    }

    public void UpdateSoundVolume()
    {
        _previuosSFXVolume = _sfx[1].volume;
        _previuosMusicVolume = _musics[1].volume;
    }

    private void UpdateSFXVolume()
    {
        _previuosSFXVolume = _sfx[1].volume;
    }

    private void UpdateMisucVolume()
    {
        _previuosMusicVolume = _musics[1].volume;
    }

}
