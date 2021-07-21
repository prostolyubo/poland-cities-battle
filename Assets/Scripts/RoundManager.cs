using System;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public Text P1HP, P2HP;
    public GameObject gameOver;

    private void Awake()
    {
        PlayerActor.OnDamageTaken += HandleDamageTaken;
    }

    private void HandleDamageTaken(PlayerActor player)
    {
        if (player.isFirst)
            HandleDamageTaken(player, P1HP);
        else
            HandleDamageTaken(player, P2HP);
    }

    private void HandleDamageTaken(PlayerActor player, Text healthDisplay)
    {
        healthDisplay.text = player.HP.ToString();
        if (player.HP > 0)
            return;

        gameOver.SetActive(true);
        Time.timeScale = 0;
    }
}