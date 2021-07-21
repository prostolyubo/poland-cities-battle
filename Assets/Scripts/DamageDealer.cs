using UnityEngine;

public abstract class DamageDealer : MonoBehaviour
{
    public float damage;
    public bool isDealing;

    protected void TryDealDamage(PlayerActor player)
    {
        if (isDealing && player != null)
            player.DealDamage(damage);
    }
}
