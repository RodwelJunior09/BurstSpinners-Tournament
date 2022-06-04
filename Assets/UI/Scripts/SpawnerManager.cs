using System.Linq;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyTeamsSpawners;
    [SerializeField] EnemySpawner _survivorEnemySpawner;
    [SerializeField] EnemySpawner _tournamentEnemySpawner;

    // Deactivate any spawner that doesn't relate to the mode selected
    private void OnEnable() {
        if (PlayerPrefs.GetInt("survivor_mode") == 1)
        {
            _enemyTeamsSpawners.gameObject.SetActive(false);
            _tournamentEnemySpawner.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("br_mode") == 1)
        {
            _survivorEnemySpawner.gameObject.SetActive(false);
            _tournamentEnemySpawner.gameObject.SetActive(false);
        }
        else
        {
            _enemyTeamsSpawners.gameObject.SetActive(false);
            _survivorEnemySpawner.gameObject.SetActive(false);
        }
    }
}
