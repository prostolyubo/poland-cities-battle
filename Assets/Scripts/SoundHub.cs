using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundHub : MonoBehaviour
{
    public UnityEvent OnPlayerJumped;
    public UnityEvent OnPlayerHit;
    public UnityEvent OnPlayerDefeated;
    public UnityEvent OnPlayerVictorious;
}
