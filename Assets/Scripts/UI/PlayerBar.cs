using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBar : MonoBehaviour
{
    public RectTransform health;
    public RectTransform stamina;

    public bool isFirst;

    private void Awake()
    {
        PlayerActor.OnDamageTaken += HandlePlayerUpdate;
        PlayerActor.OnStaminaChanged += HandlePlayerUpdate;
    }

    private void HandlePlayerUpdate(PlayerActor player)
    {
        if (player.isFirst != isFirst)
            return;

        health.localScale = new Vector3(player.HP/player.maxHP, 1, 1);
        stamina.localScale = new Vector3(player.stamina.CurrentStamina/player.stamina.maxStamina, 1, 1);
    }

}
