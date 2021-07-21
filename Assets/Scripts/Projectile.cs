using System;
using System.Collections;
using Assets;
using UnityEngine;

class Projectile : MonoBehaviour
{
    public Rigidbody2D body;
    public float shootForce;
    public Transform self;
    public float damage;
    public ParticleSystem particles;

    internal void Shoot(ProjectileActor shooter)
    {
        self.position = shooter.spawnPoint.position;
        var direction = Quaternion.Euler(0, 0, shooter.flipReference.localScale.x > 0 ? shooter.angle : -shooter.angle);
        var force = direction * (shooter.flipReference.localScale.x * shooter.spawnPoint.right) * shootForce;
        body.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(body);
        Destroy(GetComponent<Collider2D>());
        Destroy(GetComponent<SpriteRenderer>());
        StartCoroutine(FinishingRoutine());
    }

    private IEnumerator FinishingRoutine()
    {
        var emission = particles.emission;
        emission.rateOverTimeMultiplier = 0;
        while (particles.particleCount > 0)
            yield return null;
        Destroy(this.gameObject);
    }
}
