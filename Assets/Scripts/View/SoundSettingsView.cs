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

    public void SetSFXVolume()
    {
        foreach (var sfx in _sfx)
            sfx.volume = _sfxSetting.value;
    }

    public void SetMusicVolume()
    {
        foreach (var music in _musics)
            music.volume = _musicSetting.value;
    }

}
