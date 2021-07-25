using UnityEngine;

public class DamageDealerTrigger : DamageDealer
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryDealDamage(collision.attachedRigidbody);
    }
}
