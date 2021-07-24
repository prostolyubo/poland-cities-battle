using System;
using System.Collections;
using UnityEngine;
public class CooldownWeaponController : WeaponController
{
    public float cooldown;

    bool triggered;
    float left;

    private void Update()
    {
        if (triggered && left>0)
        {
            left -= Time.deltaTime;
            return;
        }
        if (triggered)
        {
            triggered = false;
            Release();
            return;
        }
        if (Input.GetKeyDown(ActionKeyCode))
        {
            left = cooldown;
            triggered = true;
            Use();
        }
    }
}