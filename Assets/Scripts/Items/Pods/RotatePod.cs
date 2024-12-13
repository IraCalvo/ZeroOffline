using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;

public class RotatePod : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        if (CursorManager.instance.mouseWorldPos.x < playerTransform.position.x)
        {
            sr.flipX = true;
            transform.localPosition = new Vector3(Mathf.Abs(transform.localPosition.x), transform.localPosition.y, transform.localPosition.z);
        }
        else
        {
            sr.flipX = false;
            transform.localPosition = new Vector3(-Mathf.Abs(transform.localPosition.x), transform.localPosition.y, transform.localPosition.z);
        }
    }
}
