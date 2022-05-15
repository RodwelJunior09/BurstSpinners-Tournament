using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject SurvivorEnemySpawner;
    [SerializeField] GameObject TournamentEnemySpawner;

    private void OnEnable() {
        if (PlayerPrefs.GetInt("survivor_mode") == 1)
            TournamentEnemySpawner.gameObject.SetActive(false);
        else
            SurvivorEnemySpawner.gameObject.SetActive(false);
    }
}
