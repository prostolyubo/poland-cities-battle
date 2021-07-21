using System;
using UnityEngine;

public class PlayerActor : MonoBehaviour
{
    public string displayName;
    public bool isFirst;
    public float movementSpeed, flightSpeed, jumpStrength;
    public Rigidbody2D target;
    public Transform root;

    public static event Action<PlayerActor> OnDamageTaken;

    public float HP;

    bool isFlipped;

    public enum Direction
    {
        NONE,
        LEFT,
        RIGHT
    }

    public void Move(Direction direction, bool isGrounded, bool jump)
    {
        float multiplier = isGrounded ? movementSpeed : flightSpeed;

        switch (direction)
        {
            case Direction.NONE:
                break;
            case Direction.LEFT:
                target.AddForce(Vector2.left * multiplier, ForceMode2D.Force);
                break;
            case Direction.RIGHT:
                target.AddForce(Vector2.right * multiplier, ForceMode2D.Force);
                break;
            default:
                break;
        }

        if(jump)
            target.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
    }

    internal void DealDamage(float damage)
    {
        HP -= damage;
        OnDamageTaken?.Invoke(this);
    }

    internal void SetFlip(bool shouldFlip)
    {
        if (shouldFlip == isFlipped)
            return;

        SetFlipCmd(shouldFlip);
    }

    private void SetFlipCmd(bool shouldFlip)
    {
        isFlipped = shouldFlip;
        root.localScale = new Vector3(isFlipped ? -1 : 1, 1, 1);
    }
}
