using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stamina : MonoBehaviour
{
    public float maxStamina;
    public float currentStamina;
    public float regenSpeed;

    public void StartReplenishing()
    {
        enabled = true;
    }

    public void Update()
    {
        currentStamina += regenSpeed * Time.deltaTime;
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
            enabled = false;
            return;
        }
    }
}