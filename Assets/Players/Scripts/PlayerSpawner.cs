using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] bool stopSpinCharacter;
    [SerializeField] GameObject[] allPlayers;

    GameObject playerSpawn;
    GameObject[] teamSpawn;

    int initialCount = 0;

    private void Awake()
    {
        SpawnPlayer();
        StopSpinning();
    }

    int GetPlayerChoice()
    {
        if (PlayerPrefs.GetInt("br_mode") != 1)
            return PlayerPrefs.GetInt("playerId");
        else 
            return PlayerPrefs.GetInt($"team_member_{initialCount}");
    }

    void SpawnPlayer()
    {
        playerSpawn = Instantiate(allPlayers[GetPlayerChoice()], transform.position, transform.rotation);
        playerSpawn.transform.parent = transform;
    }

    public void SpawnTeamPlayer(int count){
        int member_id = PlayerPrefs.GetInt($"team_member_{count}");
        playerSpawn = Instantiate(allPlayers[member_id], transform.position, transform.rotation);
        playerSpawn.transform.parent = transform;
    }

    void StopSpinning()
    {
        if (stopSpinCharacter)
        {
            playerSpawn.GetComponent<PlayerHealth>().SetToStopSpin(true);
            playerSpawn.GetComponent<Animator>().SetBool("IsStopping", true); // Decrease all the spin energy
        }
    }
}
