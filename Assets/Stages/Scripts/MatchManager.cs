using UnityEngine;

public class MatchManager : MonoBehaviour
{
    LevelManager levelManager;
    EnemySpawner enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        this.enemySpawner = FindObjectOfType<EnemySpawner>();
        this.levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("br_mode") != 1)
        {
            PlayerWonTheTournament();
            PlayerWonSurvivalMode();
        }
    }

    void PlayerWonSurvivalMode(){
        if (PlayerPrefs.GetInt("enemyId").Equals(enemySpawner.GetEnemyCount) && PlayerPrefs.GetInt("survivor_mode") == 1)
        {
            StartCoroutine(levelManager.WinSurvivalMode());
            PlayerPrefs.DeleteKey("survivor_mode");
        }
    }

    void PlayerWonTheTournament()
    {
        if (PlayerPrefs.GetInt("player_won_match") >= 3 && PlayerPrefs.GetInt("tournament_mode") == 1)
        {
            StartCoroutine(levelManager.LoadWinScreen());
            PlayerPrefs.SetInt("player_won_match", 0);
        }
    }
}
