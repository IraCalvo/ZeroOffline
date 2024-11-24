using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContact : MonoBehaviour
{
    //this is a test script for now

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //detection of collision
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("inside the if statement");
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1, DamageSource.Enemy, 0);
        }
    }
}
