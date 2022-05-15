using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    Player player;
    EnemyAI enemy;
    GameObject pausePanel;
    LevelManager levelManager;
    GameObject winLoseRoundTextPanel;

    [SerializeField] Button pauseButton;
    [SerializeField] TextMeshProUGUI roundText;

    private void Start()
    {
        this.player = FindObjectOfType<Player>();
        this.enemy = FindObjectOfType<EnemyAI>();
        this.pausePanel = GameObject.Find("PausePanel");
        this.levelManager = FindObjectOfType<LevelManager>();
        this.winLoseRoundTextPanel = GameObject.Find("WinPanel");
        this.pauseButton.onClick.AddListener(() => levelManager.PauseGame());
        pausePanel.SetActive(false);
        winLoseRoundTextPanel.SetActive(false);
    }

    void Update()
    {
        PlayerWonRound();
        PlayerLoseRound();
        PlayerPauseGame();
    }

    void PlayerPauseGame()
    {
        if (levelManager.IsGamePause())
        {
            pausePanel.SetActive(true);
            GameObject.Find("ResumeBtn").GetComponent<Button>().onClick.AddListener(() => levelManager.ResumeGame());
            GameObject.Find("QuitBtn").GetComponent<Button>().onClick.AddListener(() => levelManager.LoadHomePage());
        }
        else
            pausePanel.SetActive(false);
    }

    void PlayerWonRound()
    {
        if (player.ReturnRoundWin() == 1)
        {
            winLoseRoundTextPanel.SetActive(true);
        }
    }

    void PlayerLoseRound()
    {
        if (enemy.ReturnRoundsWon() == 1)
        {
            winLoseRoundTextPanel.SetActive(true);
            roundText.text = "You lose, this round!!";
        }
    }
}
