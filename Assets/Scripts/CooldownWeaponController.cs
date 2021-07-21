using System;
using System.Collections;
using UnityEngine;
public class CooldownWeaponController : WeaponController
{
    public float cooldown;
    private IEnumerator Start()
    {
        yield return ProjectileCycleRoutine();
    }

    private IEnumerator ProjectileCycleRoutine()
    {
        while (!Input.GetKeyDown(ActionKeyCode))
            yield return null;
        Use();
        yield return new WaitForSeconds(cooldown);
        Release();
        yield return ProjectileCycleRoutine();
    }
}