using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] bool stopSpinCharacter;
    [SerializeField] GameObject[] allPlayers;

    GameObject playerSpawn;
    [SerializeField] GameObject[] teamSpawn;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("br_mode") != 1) { SpawnPlayer(); }
        // else { SpawnTeamPlayer(0); }
        StopSpinning();
        SetPlayerTeamIds();
    }

    int GetPlayerChoice()
    {
        return PlayerPrefs.GetInt("playerId");
    }

    void SetPlayerTeamIds(){
        if (PlayerPrefs.GetInt("br_mode") == 1)
        {
            for (int count = 0; count < 3; count++)
            {
                int member_id = PlayerPrefs.GetInt($"team_member_{count}");
                Debug.Log(member_id);
                // teamSpawn[count] = allPlayers[member_id];
            }
        }
    }

    void SpawnPlayer()
    {
        playerSpawn = Instantiate(allPlayers[GetPlayerChoice()], transform.position, transform.rotation);
        playerSpawn.transform.parent = transform;
    }

    void SpawnTeamPlayer(int member_count){
        Debug.Log(member_count);
        // int member_id = teamSpawn[member_count];
        // playerSpawn = Instantiate(allPlayers[member_id], transform.position, transform.rotation);
        // playerSpawn.transform.parent = transform;
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
