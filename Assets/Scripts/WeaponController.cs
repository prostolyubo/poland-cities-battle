using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    public event Action<Action> OnUseTriggered;

    protected bool isLocked = false;
    public bool IsLocked => isLocked;
    [SerializeField]
    ControlSchemeDealer controls;

    [SerializeField]
    bool isPrimary;
    private Coroutine failsafe;

    protected KeyCode ActionKeyCode => isPrimary ? controls.scheme.primary : controls.scheme.secondary;

    protected void Use()
    {
        isLocked = true;
        OnUseTriggered?.Invoke(Release);
        if (failsafe != null)
            failsafe = StartCoroutine(Failsafe());
    }

    private IEnumerator Failsafe()
    {
        yield return new WaitForSecondsRealtime(5);
        Release();
    }

    protected void Release()
    {
        isLocked = false;
        if (failsafe == null)
            return;
        StopCoroutine(failsafe);
        failsafe = null;
    }
}
