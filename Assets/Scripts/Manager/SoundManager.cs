using UnityEditorInternal;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioclip, Transform spawntransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawntransform.position, Quaternion.identity);
        audioSource.clip = audioclip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
