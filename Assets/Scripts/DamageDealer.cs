using System;
using UnityEngine;

public abstract class DamageDealer : MonoBehaviour
{
    public float damage;
    public bool isDealing;

    public event Action OnHit;

    protected void TryDealDamage(PlayerActor player)
    {
        OnHit?.Invoke();
        if (isDealing && player != null)
            player.DealDamage(damage);
    }
}
