using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public void ResumeButton()
    {
        Time.timeScale = 1;
        PlayerUIManager.instance.pauseMenu.gameObject.SetActive(false);
    }

    public void KeyBindingButton()
    {
        
    }

    public void ExitButton()
    {
        
    }
}
