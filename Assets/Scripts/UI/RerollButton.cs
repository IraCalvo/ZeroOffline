using UnityEngine;

public class RerollButton : MonoBehaviour
{
    public void ButtonPresed()
    {
        if (LevelupManager.instance.rerollAmount > 0)
        {
            LevelupManager.instance.RerollChoices();
        }
    }
}
