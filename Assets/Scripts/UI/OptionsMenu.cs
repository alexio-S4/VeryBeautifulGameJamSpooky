using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Références UI")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        if (SoundManager.instance != null)
        {
            masterSlider.value = SoundManager.instance.masterVolume;
            musicSlider.value = SoundManager.instance.musicVolume;
            sfxSlider.value = SoundManager.instance.sfxVolume;
        }

        masterSlider.onValueChanged.AddListener(SetMaster);
        musicSlider.onValueChanged.AddListener(SetMusic);
        sfxSlider.onValueChanged.AddListener(SetSFX);
    }

    private void SetMaster(float value) { SoundManager.instance.SetMasterVolume(value); }
    private void SetMusic(float value) { SoundManager.instance.SetMusicVolume(value); }
    private void SetSFX(float value) { SoundManager.instance.SetSFXVolume(value); }
}