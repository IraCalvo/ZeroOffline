using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : Singleton<CursorManager>
{
    public Vector2 mousePos;
    public Vector2 mouseWorldPos;

    private void Start()
    {
        //if (Application.isPlaying)
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //}
        //else
        //{
        //    Cursor.lockState = CursorLockMode.Confined;
        //}
    }

    private void Update()
    {
        mousePos = Input.mousePosition;

        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
    }
}
