using UnityEngine;

public class DamageDealerCollider : DamageDealer
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerActor>();
        TryDealDamage(player);
    }
}