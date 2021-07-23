using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatAnimator : PlayerAnimator
{
    public int attackState;
    public int finalAttackFrame;
    public SimpleWeaponController weaponController;
    public Transform attackPoint;
    public float attackRadius;
    public float damage;

    Action releaseWeaponCallback;

    protected override void Awake()
    {
        base.Awake();
        weaponController.OnUseTriggered += Attack;
    }

    public void Attack(Action callback)
    {
        releaseWeaponCallback = callback;
        this.spriteAnimator.OnFrame += HandleFrameChange;
        spriteAnimator.SetState(attackState);
    }

    private void HandleFrameChange(int state, int frame)
    {
        if (state == 0)
        {
            releaseWeaponCallback?.Invoke();
            this.spriteAnimator.OnFrame -= HandleFrameChange;
            return;
        }
        if (state != attackState || frame != finalAttackFrame)
            return;
        var hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius);
        foreach (var hit in hits)
        {
            var player = hit.gameObject.GetComponent<PlayerActor>();
            if (player == null)
                continue;
            player.DealDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
