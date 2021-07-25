﻿using System;
using UnityEngine;

public abstract class DamageDealer : MonoBehaviour
{
    public float damage;
    public bool isDealing;
    public bool knockback;
    public float knockbackForce;
    public Vector2 knockbackDirection;

    public event Action OnHit;

    public static event Action<Vector3> OnPlayerHit;

    protected void TryDealDamage(PlayerActor player)
    {
        OnHit?.Invoke();
        if (isDealing && player != null)
        {
            OnPlayerHit?.Invoke(transform.position);
            player.DealDamage(damage);
        }

        if (knockback && player != null)
            player.Knockback(knockbackForce, knockbackDirection);
    }
}
