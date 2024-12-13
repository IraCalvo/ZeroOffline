using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        RotateWeapon();
    }

    void RotateWeapon()
    {
        Vector2 direction = CursorManager.instance.mouseWorldPos - (Vector2)playerTransform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (CursorManager.instance.mouseWorldPos.x < playerTransform.position.x)
        {
            sr.flipY = true;
            transform.localPosition = new Vector3(-Mathf.Abs(transform.localPosition.x), transform.localPosition.y, transform.localPosition.z);
        }
        else
        {
            sr.flipY = false;
            transform.localPosition = new Vector3(Mathf.Abs(transform.localPosition.x), transform.localPosition.y, transform.localPosition.z);
        }
    }
}


//bool mouseLeft = CursorManager.Instance.mouseWorldPos.x < playerTransform.position.x;
//bool mouseAbove = CursorManager.Instance.mouseWorldPos.y > playerTransform.position.y;

//sr.flipX = mouseLeft;
//sr.flipY = !mouseAbove;
