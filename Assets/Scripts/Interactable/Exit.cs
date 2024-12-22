using UnityEngine;

public class Exit : Interactable
{
    public override void Interact()
    {
        UIManager.instance.exitConfirmationPanel.gameObject.SetActive(true);
    }
}
