using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _enemiesInTournament;

    private void Awake()
    {
        SpawnEnemy(PlayerPrefs.GetInt("enemyId"));
    }

    void SpawnEnemy(int index)
    {
        GameObject enemyAI = Instantiate(_enemiesInTournament[index], transform.position, transform.rotation) as GameObject;
        enemyAI.transform.parent = transform;
    }

    public int GetEnemyCount => _enemiesInTournament.Length;
}
