using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public GameObject gameOver;
    public PlayerActor first, second;
    public Text firstName, secondName, winnerName;
    public Map map;
    public ControlScheme firstScheme, secondScheme;
    public string winMessage;
    public bool isIngame;
    public Map[] maps;
    public PlayerActor[] actors;
    public Image winnerAvatar;

    [ContextMenu("Start")]
    public void StartRound()
    {
        if (first == null)
        {
            first = actors[Random.Range(0, actors.Length)];
        }
        if (second == null)
        {
            second = actors[Random.Range(0, actors.Length)];
        }
        map = Instantiate(maps[UnityEngine.Random.Range(0, maps.Length)]);
        first = Instantiate(first, map.spawn1.position, Quaternion.identity, null);
        second = Instantiate(second, map.spawn2.position, Quaternion.identity, null);
        first.GetComponent<ControlSchemeDealer>().scheme = firstScheme;
        second.GetComponent<ControlSchemeDealer>().scheme = secondScheme;
        first.GetComponent<PlayerController>().manager = this;
        second.GetComponent<PlayerController>().manager = this;
        first.isFirst = true;
        firstName.text = first.displayName;
        secondName.text = second.displayName;
        isIngame = true;
    }

    private void Awake()
    {
        PlayerActor.OnDamageTaken += HandleDamageTaken;
    }
  
    private void HandleDamageTaken(PlayerActor player)
    {
        if (player.HP > 0)
            return;

        PlayerActor winner;
        if (player == first)
            winner = second;
        else
            winner = first;

        winnerName.text = string.Format(winMessage.Replace("\\n", "\n"), winner.displayName);
        winnerAvatar.sprite = winner.avatar;

        gameOver.SetActive(true);
        Time.timeScale = 0;
        Camera.main.GetComponentInParent<CameraController>().enabled = false;
    }
}