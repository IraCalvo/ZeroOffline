using UnityEngine;

public enum DamageOwner
{
    Player,
    Enemy,
    Neutral
}

[RequireComponent(typeof(Collider2D))]
public class DamageSource : MonoBehaviour
{
    public int dmgToDeal;
    public float critChance;
    public DamageOwner owner;

    public void DealDamage(Collider2D otherCollision, float dmg, float crit, DamageOwner dmgOwner)
    {
        
    }
}
