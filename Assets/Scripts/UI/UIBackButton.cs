using UnityEngine;

public class UIBackButton : MonoBehaviour
{
    public GameObject currentOpenObject;
    public GameObject thingToGoBackToo;

    public void ButtonPressed()
    {
        currentOpenObject.SetActive(false);
        thingToGoBackToo.SetActive(true);
    }
}
