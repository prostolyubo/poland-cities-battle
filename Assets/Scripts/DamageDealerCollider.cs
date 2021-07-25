using UnityEngine;

public class DamageDealerCollider : DamageDealer
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryDealDamage(collision.rigidbody);
    }
}