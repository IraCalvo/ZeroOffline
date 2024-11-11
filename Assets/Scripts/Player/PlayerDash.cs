using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed;
    public float dashDuration;
    public float dashCD;
    [HideInInspector] bool isDashing;
    float startingSpeed;

    public virtual void Dash()
    {
        if (isDashing == false)
        {
            startingSpeed = PlayerController.instance.playerMS;
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        isDashing = true;
        PlayerController.instance.playerMS *= dashSpeed;
        PlayerHealth.instance.playerCanBeHit = false;

        yield return new WaitForSeconds(dashDuration);
        PlayerController.instance.playerMS = startingSpeed;
        PlayerHealth.instance.playerCanBeHit = true;

        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
