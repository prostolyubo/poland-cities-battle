using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public SpriteAnimator spriteAnimator;
    public PlayerController controller;

    public int jumpState;
    public int walkState;

    protected virtual void Awake()
    {
        controller.OnGroundChanged += HandleGroundingChange;
        controller.OnWalkingChanged += HandleWalkingChange;
    }

    private void HandleWalkingChange(bool isWalking)
    {
        if (!controller.isGrounded)
            return;
        spriteAnimator.SetState(isWalking ? walkState : 0);
    }

    private void HandleGroundingChange(bool isGrounded)
    {
        if (isGrounded)
            spriteAnimator.SetState(controller.isWalking ? walkState : 0);
        else
            spriteAnimator.SetState(jumpState);
    }
}
