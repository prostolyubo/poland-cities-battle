using System;
using System.Collections;
using UnityEngine;
public class HoldWeaponController : WeaponController
{
    public event Action OnDisengaged;

    void Update()
    {
        if (isLocked)
        {
            if (Input.GetKeyUp(ActionKeyCode))
                OnDisengaged?.Invoke();
        }
        else {
            if (Input.GetKeyDown(ActionKeyCode))
                Use();
        }
    }
}
