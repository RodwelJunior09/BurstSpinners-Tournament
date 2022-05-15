using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEnemyRounds : MonoBehaviour
{
    EnemyAI enemyAI;
    RawImage[] enemyRounds;

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = FindObjectOfType<EnemyAI>();
        enemyRounds = GetComponentsInChildren<RawImage>().Where(img => img.CompareTag("EnemyRounds")).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayEnemyRoundWonScore();
    }

    void DisplayEnemyRoundWonScore()
    {
        int roundsWon = PlayerPrefs.GetInt("rounds_won_by_enemy");
        if (roundsWon > 0)
        {
            int count = 1;
            while (count <= roundsWon)
            {
                enemyRounds[count - 1].color = Color.red;
                count++;
            }
        }
    }
}
