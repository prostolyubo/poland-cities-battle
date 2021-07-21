using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeActor : MonoBehaviour
{
    public WeaponController controller;
    public DamageDealer dealer;

    Action callback;
    public Animation animator;

    private void Awake()
    {
        controller.OnUseTriggered += HandleUse;
    }

    private void HandleUse(Action callback)
    {
        dealer.isDealing = true;
        this.callback = callback;
        animator.Play();
    }


    public void FinishMove()
    {
        callback?.Invoke();
        callback = null;
        dealer.isDealing = false;
    }
}
