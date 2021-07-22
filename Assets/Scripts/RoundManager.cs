using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public GameObject gameOver;
    public PlayerActor first, second;
    public Text firstName, secondName, winnerName;
    public string winMessage;

    [ContextMenu("Start")]
    public void StartRound()
    {
        firstName.text = first.displayName;
        secondName.text = second.displayName;
    }
    private void Awake()
    {
        PlayerActor.OnDamageTaken += HandleDamageTaken;
    }

  
    private void HandleDamageTaken(PlayerActor player)
    {
        if (player.HP > 0)
            return;

        string winner;
        if (player == first)
            winner = second.displayName;
        else
            winner = first.displayName;

        winnerName.text = string.Format(winMessage.Replace("\\n", "\n"), winner);

        gameOver.SetActive(true);
        Time.timeScale = 0;
    }
}