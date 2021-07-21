using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    public event Action<Action> OnUseTriggered;

    protected bool isLocked = false;
    [SerializeField]
    ControlScheme controls;

    [SerializeField]
    bool isPrimary;

    protected KeyCode ActionKeyCode => isPrimary ? controls.primary : controls.secondary;

    protected void Use()
    {
        isLocked = true;
        OnUseTriggered?.Invoke(Release);
    }

    protected void Release()
    {
        isLocked = false;
    }
}
