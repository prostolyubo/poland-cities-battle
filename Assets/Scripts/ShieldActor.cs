using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldActor : MonoBehaviour
{
    public HoldWeaponController controller;
    public Stamina stamina;
    Action callback;
    public Animation animator;
    bool isReversed;
    bool isShielding;

    private void Awake()
    {
        controller.OnUseTriggered += HandleUse;
        controller.OnDisengaged += HandleDisengage;
    }

    private void HandleDisengage()
    {
        isShielding = false;
        if (isReversed)
            return;

        animator[animator.clip.name].speed = -1;
        isReversed = true;
        if (!animator.isPlaying)
        {
            animator[animator.clip.name].normalizedTime = 1;
            animator.Play();
        }
    }

    private void Update()
    {
        if (!isShielding)
            return;

        stamina.currentStamina -= Time.deltaTime;
        if (stamina.currentStamina <= 0)
        {
            stamina.currentStamina = 0;
            HandleDisengage();
        }
    }

    private void HandleUse(Action callback)
    {
        this.callback = callback;
        animator.Play();
    }

    public void NotifyShieldEngaged()
    {
        if (isReversed)
            return;
        isShielding = true;
    }

    public void AnimationStart()
    {
        if (!isReversed)
            return;
        stamina.StartReplenishing();
        isReversed = false;
        animator[animator.clip.name].speed = 1;
        animator.Stop();
        FinishMove();
    }

    void FinishMove()
    {
        callback?.Invoke();
        callback = null;
    }
}
