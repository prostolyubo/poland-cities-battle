using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WigglyDriver : MonoBehaviour
{
    public float osscillation, angle, force, period, forward;
    public Rigidbody2D body;
    public Transform flipReference;

    bool side;
    Vector2 baseUpDir, baseDownDir, direction, startDirection, desiredDirection;
    float currentPeriod;

    private void Awake()
    {
        baseUpDir = Rotate(Vector2.right, -angle);
        baseDownDir = Rotate(Vector2.right, angle);
        period += (2 * Random.value - 1) * osscillation * period;
        currentPeriod = period * osscillation;
        NewDirection();
        startDirection = desiredDirection * osscillation;
        desiredDirection = flipReference.right * force;
        side = Random.value < 0.5f;
    }

    void FixedUpdate()
    {
        direction = Vector2.Lerp(desiredDirection, startDirection, currentPeriod / period);
        body.velocity = direction;

        currentPeriod -= Time.fixedDeltaTime;
        if (currentPeriod > 0)
            return;

        period += (2 * Random.value - 1) * osscillation * period;
        currentPeriod = period;
        NewDirection();
        startDirection = body.velocity;

        side = !side;
    }

    private void NewDirection()
    {
        desiredDirection = Rotate(side ? baseUpDir : baseDownDir, Random.value * angle * osscillation);
        desiredDirection += Vector2.right * forward;
        desiredDirection.x *= flipReference.localScale.x;
    }

    public static Vector2 Rotate(Vector2 v, float degrees)
    {
        return Quaternion.Euler(0, 0, degrees) * v;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(flipReference.position, direction * force * 5);
    }
}
