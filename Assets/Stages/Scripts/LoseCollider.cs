using UnityEngine;

public class LoseCollider : MonoBehaviour
{
    Player player;
    EnemyAI enemyAI;
    LevelManager levelManager;

    bool objectCollided = false;

    private void Start()
    {
        this.enemyAI = FindObjectOfType<EnemyAI>();
        this.levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.GetComponent<EnemyAI>())
        {
            if (!objectCollided)
            {
                objectCollided = true;
                if (PlayerPrefs.GetInt("br_mode") != 1)
                {
                    player.IncreaseAmountOfWins();
                    StartCoroutine(levelManager.TournamentManager());
                }
            }
        }
        else if (otherCollider.gameObject.GetComponent<Player>())
        {
            if (!objectCollided)
            {
                objectCollided = true;
                if (PlayerPrefs.GetInt("br_mode") != 1)
                {
                    enemyAI.IncreaseRoundWon();
                    StartCoroutine(levelManager.TournamentManager());
                }
                else {
                    otherCollider.gameObject.GetComponent<PlayerHealth>().SetToStopSpin(true); // Make the player spinner stop spinning when falling.
                }
                
            }
        }
        if (PlayerPrefs.GetInt("br_mode") == 1)
            objectCollided = false;
    }
}
