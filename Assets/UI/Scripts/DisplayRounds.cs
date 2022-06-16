using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRounds : MonoBehaviour
{
    RawImage[] playerRounds;

    // Start is called before the first frame update
    void Start()
    {
        playerRounds = GetComponentsInChildren<RawImage>().Where(img => img.CompareTag("PlayerRounds")).ToArray();
    }

    void Update()
    {
        DisplayPlayerRoundScore();
    }

    void DisplayPlayerRoundScore()
    {
        if (PlayerPrefs.GetInt("br_mode") != 1)
        {
            int roundsWon = PlayerPrefs.GetInt("rounds_won_by_player");
            if (roundsWon > 0)
            {
                int count = 1;
                while (count <= roundsWon)
                {
                    playerRounds[count - 1].color = Color.red;
                    count++;
                }
            }
        }
        else {
            this.gameObject.SetActive(false);
        }
    }
}
