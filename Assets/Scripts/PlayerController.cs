using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isGrounded;
    public bool isWalking;
    public Transform feet, root;

    public Rigidbody2D body;
    public float maxJumpVelocity = 5f;

    bool isFlipped;

    ContactPoint2D[] contacts = new ContactPoint2D[1];

    public PlayerActor target;

    public ControlSchemeDealer controls;
    public event Action<bool> OnWalkingChanged;
    public event Action<bool> OnGroundChanged;

    public RoundManager manager { get; set; }
    public bool skipMapCheck;

    void FixedUpdate()
    {
        SetGround(body.GetContacts(contacts) > 0);

        if (!(Input.anyKey || Input.anyKeyDown))
        {
            SetWalking(false);
            return;
        }

        PlayerActor.Direction direction = PlayerActor.Direction.NONE;
        if (Input.GetKey(controls.scheme.left))
        {
            direction = PlayerActor.Direction.LEFT;
            target.SetFlip(true);
            SetWalking(true);
        }
        else if (Input.GetKey(controls.scheme.right))
        {
            direction = PlayerActor.Direction.RIGHT;
            target.SetFlip(false);
            SetWalking(true);
        }
        else
            SetWalking(false);
        target.Move(direction, isGrounded, false);
    }

    void SetWalking(bool newWalkingState)
    {
        if (isWalking == newWalkingState)
            return;
        isWalking = newWalkingState;
        OnWalkingChanged?.Invoke(isWalking);
    }

    void SetGround(bool newGroundState)
    {
        if (isGrounded == newGroundState)
            return;
        isGrounded = newGroundState;
        OnGroundChanged?.Invoke(isGrounded);
    }

    private void Update()
    {
        if (isGrounded && Input.GetKeyDown(controls.scheme.jump))
            target.Move(PlayerActor.Direction.NONE, isGrounded, true);

        if (body.velocity.y > maxJumpVelocity)
            body.velocity = new Vector2(body.velocity.x, maxJumpVelocity);

        if (!skipMapCheck && root.transform.position.y < manager.map.lowerBound.position.y)
            target.DealDamage(100);
    }
}
