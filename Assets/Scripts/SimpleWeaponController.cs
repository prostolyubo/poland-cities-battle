using System;
using System.Collections;
using UnityEngine;
public class SimpleWeaponController : WeaponController
{
    public AK.Wwise.Event Play_sound;
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
            Play_sound.Post(gameObject);
        }
    }
}
