using System;
using UnityEngine;

public class PlayerActor : MonoBehaviour
{
    public string displayName;
    public bool isFirst;
    public float movementSpeed, flightSpeed, jumpStrength;
    public Rigidbody2D target;
    public Transform root;
    public Stamina stamina;

    public static event Action<PlayerActor> OnDamageTaken;

    public float HP;
    public float maxHP;
    public Sprite avatar;

    bool isFlipped;

    public static event Action<PlayerActor> OnStaminaChanged;

    public enum Direction
    {
        NONE,
        LEFT,
        RIGHT
    }

    internal void Knockback(float knockbackForce, Vector2 knockbackDirection)
    {
        knockbackDirection.Scale(root.localScale);
        target.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }

    private void Awake()
    {
        stamina.OnStaminaChanged += (float _) => OnStaminaChanged?.Invoke(this);
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
        if (HP < 0)
            HP = 0;
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
