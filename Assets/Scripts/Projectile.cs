using System;
using System.Collections;
using Assets;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D body;
    public Transform self;
    public float damage;
    public ParticleTerminator particleTerminator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(body);
        Destroy(GetComponent<Collider2D>());
        Destroy(GetComponent<SpriteRenderer>());
        Destroy(GetComponent<SpriteAnimator>());
        particleTerminator.Terminate(() => Destroy(this.gameObject));
    }
}
