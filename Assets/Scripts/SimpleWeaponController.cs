using System;
using System.Collections;
using UnityEngine;
public class SimpleWeaponController : WeaponController
{
    void Update()
    {
        if (!isLocked && Input.GetKeyDown(ActionKeyCode))
        {
            Use();
        }
    }
}
