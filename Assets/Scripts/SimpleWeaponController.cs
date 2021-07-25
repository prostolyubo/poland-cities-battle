using System;
using System.Collections;
using UnityEngine;
public class SimpleWeaponController : WeaponController
{
    public WeaponController other;
    public bool blockWithOther;

    void Update()
    {
        if(blockWithOther && other.IsLocked)
        {
            return;
        }

        if (!isLocked && Input.GetKeyDown(ActionKeyCode))
        {
            Use();
        }
    }
}
