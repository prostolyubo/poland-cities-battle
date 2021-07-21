using UnityEngine;

public class DamageDealerTrigger : DamageDealer
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerActor>();
        TryDealDamage(player);
    }
}
