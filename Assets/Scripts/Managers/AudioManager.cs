using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource masterAudio;
    public AudioSource sfxAudio;
    public AudioSource musicAudio;

    public void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
    }

    private void Start()
    {
        masterAudio.volume = PlayerPrefs.GetFloat("MasterVolume");
        sfxAudio.volume = PlayerPrefs.GetFloat("SFXVolume");
        musicAudio.volume = PlayerPrefs.GetFloat("MusicVolume");
    }
}
