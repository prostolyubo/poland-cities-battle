using System;
using UnityEngine;

public abstract class DamageDealer : MonoBehaviour
{
    public float damage;
    public bool isDealing;
    public bool knockback;
    public float knockbackForce;
    public Vector2 knockbackDirection;

    public event Action OnHit;

    protected void TryDealDamage(PlayerActor player)
    {
        OnHit?.Invoke();
        if (isDealing && player != null)
            player.DealDamage(damage);

        if (knockback && player != null)
            player.Knockback(knockbackForce, knockbackDirection);
    }
}
