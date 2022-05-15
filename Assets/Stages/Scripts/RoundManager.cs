using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    LevelManager levelManager;
    AdvertisementManager adsManager;

    [SerializeField] bool playerWin;

    // Start is called before the first frame update
    void Start()
    {
        this.levelManager = FindObjectOfType<LevelManager>();
        this.adsManager = FindObjectOfType<AdvertisementManager>();
    }

    private void Update()
    {
        PlayerLoseMatch();
        PlayerWinsMatch();
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
            DeleteEnemyId();
            DeleteRoundData();
            StartCoroutine(levelManager.PlayerLoseScreen());
        }
        if (roundWonByOponent >= 2 && roundWonByOponent > roundWonByPlayer)
        {
            DeleteEnemyId();
            DeleteRoundData();
            StartCoroutine(levelManager.PlayerLoseScreen());
        }
    }

    public void DeleteRoundData()
    {
        PlayerPrefs.DeleteKey("rounds_won_by_player");
        PlayerPrefs.DeleteKey("rounds_won_by_enemy");
    }

    void DeleteEnemyId()
    {
        PlayerPrefs.DeleteKey("enemyId");
    }

    void LoadNextEnemy()
    {
        var nextEnemyId = PlayerPrefs.GetInt("enemyId") + 1;
        PlayerPrefs.SetInt("enemyId", nextEnemyId);
        DeleteRoundData();
        IncreaseMatch();
        StartCoroutine(LoadAdsWithTimer());
        levelManager.LoadTournament();
    }

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
