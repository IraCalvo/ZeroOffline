using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image pauseMenu;
    public Image pauseScrim;

    public GameObject basicPauseButtonGroup;
    public GameObject settingsButtonGroup;
    public GameObject audioButtonGroup;

    public Slider masterVolume;
    public Slider sfxVolume;
    public Slider musicVolume;

    private void Awake()
    {
        if (instance == null) {  instance = this; }
        else { Destroy(this); }
    }

    public void PauseGame()
    {
        if (!pauseMenu.gameObject.activeInHierarchy)
        {
            pauseMenu.gameObject.SetActive(true);
            pauseScrim.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.gameObject.SetActive(false);
            pauseScrim.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void MasterVolumeSlider(float volume)
    {
        
    }

    public void SFXVolumeSlider()
    { 
    
    }

    public void MusicVolumeSlider()
    {
        
    }
}
