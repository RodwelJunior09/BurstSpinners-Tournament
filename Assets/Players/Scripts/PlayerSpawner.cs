using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] bool stopSpinCharacter;
    [SerializeField] GameObject[] allPlayers;

    GameObject playerSpawn;

    private void Awake()
    {
        SpawnPlayer();
        StopSpinning();
    }

    int GetPlayerChoice()
    {
        return PlayerPrefs.GetInt("playerId");
    }

    void SpawnPlayer()
    {
        playerSpawn = Instantiate(allPlayers[GetPlayerChoice()], transform.position, transform.rotation);
        playerSpawn.transform.parent = transform;
    }

    void StopSpinning()
    {
        if (stopSpinCharacter)
        {
            playerSpawn.GetComponent<Player>().stopSpinning = true;
            playerSpawn.GetComponent<Animator>().SetBool("IsStopping", true); // Decrease all the spin energy
        }
    }
}
