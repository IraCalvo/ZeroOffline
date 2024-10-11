using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : BaseShooter
{
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawSphere(transform.position, currentAttackRange);
    }
}
