using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;
    public Vector2 mousePos;
    public Vector2 mouseWorldPos;
    public Vector2 aimDir;
    [SerializeField] FieldOfView fieldOfView;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(this);
        }
    }

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

        Vector2 directionToMouse = mouseWorldPos - (Vector2)PlayerController.instance.transform.position;
        aimDir = directionToMouse.normalized;

        fieldOfView.SetAimDirection(aimDir);
        fieldOfView.SetOrigin(transform.position);
    }
}
