using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyTeamsSpawners;
    [SerializeField] EnemySpawner _survivorEnemySpawner;
    [SerializeField] EnemySpawner _tournamentEnemySpawner;

    // Deactivate any spawner that doesn't relate to the mode selected
    private void OnEnable() {
        if (PlayerPrefs.GetInt("survivor_mode") == 1)
            _survivorEnemySpawner.gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("br_mode") == 1)
            _enemyTeamsSpawners.gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("tournament_mode") == 1)
            _tournamentEnemySpawner.gameObject.SetActive(true);
    }
}
