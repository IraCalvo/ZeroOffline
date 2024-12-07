using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public void ResumeButton()
    {
        Time.timeScale = 1;
        UIManager.instance.pauseMenu.gameObject.SetActive(false);
        UIManager.instance.pauseScrim.gameObject.SetActive(false);
    }

    public void SettingsButtons()
    {
        UIManager.instance.basicPauseButtonGroup.SetActive(false);
        UIManager.instance.settingsButtonGroup.SetActive(true);
    }

    public void KeyBindingButton()
    {

    }

    public void AudioSettingButton()
    {
    
    }

    public void VideoSettingButton()
    { 
    
    }

    public void ExitButton()
    {
        //TODO: bring up a popup thatll ask them if they want to leave and that none of the items they took into the mission wont be brought back
    }

}
