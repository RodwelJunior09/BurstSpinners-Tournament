using UnityEngine;

public class LoseCollider : MonoBehaviour
{
    Player player;
    EnemyAI enemyAI;
    LevelManager levelManager;

    bool objectCollided = false;

    private void Start()
    {
        this.player = FindObjectOfType<Player>();
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
                player.IncreaseAmountOfWins();
                StartCoroutine(levelManager.TournamentManager());
            }
        }
        else if (otherCollider.gameObject.GetComponent<Player>())
        {
            if (!objectCollided)
            {
                objectCollided = true;
                enemyAI.IncreaseRoundWon();
                StartCoroutine(levelManager.TournamentManager());
            }
        }
    }
}
