using System;
using System.Collections;
using UnityEngine;
public class SimpleWeaponController : WeaponController
{
    public AK.Wwise.Event Play_sound;
    void Update()
    {
        if (!isLocked && Input.GetKeyDown(ActionKeyCode))
        {
            Use();
            Play_sound.Post(gameObject);
        }
    }
}
