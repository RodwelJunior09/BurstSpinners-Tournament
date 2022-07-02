using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    PlayerSpawner playerSpawner;
    LevelManager levelManager;
    AdvertisementManager adsManager;
    [SerializeField] bool playerWin;
    int player_team_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.levelManager = FindObjectOfType<LevelManager>();
        this.playerSpawner = FindObjectOfType<PlayerSpawner>();
        this.adsManager = FindObjectOfType<AdvertisementManager>();
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("br_mode") != 1)
        {
            PlayerLoseMatch();
            PlayerWinsMatch();
        }
    }

    void PlayerWinsMatch()
    {
        var roundWonByPlayer = PlayerPrefs.GetInt("rounds_won_by_player");
        var roundWonByOponent = PlayerPrefs.GetInt("rounds_won_by_enemy");
        if (roundWonByPlayer >= 3)
            LoadNextEnemy();
        if (roundWonByPlayer >= 2 && roundWonByPlayer > roundWonByOponent)
            LoadNextEnemy();
    }

    void PlayerLoseMatch()
    {
        var roundWonByPlayer = PlayerPrefs.GetInt("rounds_won_by_player");
        var roundWonByOponent = PlayerPrefs.GetInt("rounds_won_by_enemy");
        if (roundWonByOponent >= 3)
        {
            levelManager.DeleteEnemyId();
            levelManager.DeleteRoundData();
            StartCoroutine(levelManager.PlayerLoseScreen());
        }
        if (roundWonByOponent >= 2 && roundWonByOponent > roundWonByPlayer)
        {
            levelManager.DeleteEnemyId();
            levelManager.DeleteRoundData();
            StartCoroutine(levelManager.PlayerLoseScreen());
        }
    }

    void LoadNextEnemy()
    {
        var nextEnemyId = PlayerPrefs.GetInt("enemyId") + 1;
        PlayerPrefs.SetInt("enemyId", nextEnemyId);
        levelManager.DeleteRoundData();
        IncreaseMatch();
        StartCoroutine(LoadAdsWithTimer());
        levelManager.LoadTournament();
    }

    // Make the necessary functions when the player looses an new team member appears, reset the UI IF NECESSARY

    IEnumerator LoadAdsWithTimer()
    {
        yield return new WaitForSeconds(3);
        adsManager.ShowInterAds();
    }

    void IncreaseMatch()
    {
        if (!PlayerPrefs.HasKey("player_won_match"))
            PlayerPrefs.SetInt("player_won_match", 1);
        else
        {
            var currentScore = PlayerPrefs.GetInt("player_won_match");
            PlayerPrefs.SetInt("player_won_match", currentScore + 1);
        }
    }
}
