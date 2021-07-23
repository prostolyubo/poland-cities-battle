using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stamina : MonoBehaviour
{
    public float maxStamina;
    public float CurrentStamina
    {
        get { return currentStamina; }
        set {
            currentStamina = value;
            OnStaminaChanged?.Invoke(currentStamina);
        }
    }
    public float regenSpeed;
    [SerializeField]
    private float currentStamina;

    public event Action<float> OnStaminaChanged;
    public void StartReplenishing()
    {
        enabled = true;
    }

    public void Update()
    {
        CurrentStamina += regenSpeed * Time.deltaTime;
        if (CurrentStamina > maxStamina)
        {
            CurrentStamina = maxStamina;
            enabled = false;
            return;
        }
    }
}
