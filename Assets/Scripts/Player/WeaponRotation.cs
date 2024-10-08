using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    private SpriteRenderer sr;
    Vector2 weaponPos;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        weaponPos = gameObject.transform.position;
    }

    private void Update()
    {
        RotateWeapon();
    }

    void RotateWeapon()
    {
        float angle = Mathf.Atan2(CursorManager.Instance.mouseWorldPos.y, CursorManager.Instance.mouseWorldPos.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (CursorManager.Instance.mouseWorldPos.x < playerTransform.transform.position.x)
        {
            weaponPos.x = -Mathf.Abs(gameObject.transform.position.x);
            sr.flipY = true;
        }
        else
        {
            sr.flipY = false;
            weaponPos.x = Mathf.Abs(gameObject.transform.position.x);
        }

        gameObject.transform.position = new Vector2(weaponPos.x, weaponPos.y);
    }
}
