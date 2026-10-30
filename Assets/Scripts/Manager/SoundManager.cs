using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] private AudioSource musicAudioSource;

    [HideInInspector] public float masterVolume = 1f;
    [HideInInspector] public float musicVolume = 1f;
    [HideInInspector] public float sfxVolume = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            LoadVolumes();
        }
    }

    public void PlaySoundFXClip(AudioClip audioclip, Transform spawntransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawntransform.position, Quaternion.identity);
        audioSource.clip = audioclip;

        // Applique les modificateurs de volume globaux
        audioSource.volume = volume * masterVolume * sfxVolume;

        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        UpdateMusicVolume();
        SaveVolumes();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        UpdateMusicVolume();
        SaveVolumes();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        SaveVolumes();
    }

    private void UpdateMusicVolume()
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = musicVolume * masterVolume;
        }
    }

    private void LoadVolumes()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        UpdateMusicVolume();
    }

    private void SaveVolumes()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }
}