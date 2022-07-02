using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _enemiesInTournament;
    int current_enemy_id = 0;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("br_mode") != 1)
            SpawnEnemy(PlayerPrefs.GetInt("enemyId"));
        else
            SpawnEnemy(current_enemy_id);
    }

    void SpawnEnemy(int index)
    {
        GameObject enemyAI = Instantiate(_enemiesInTournament[index], transform.position, transform.rotation) as GameObject;
        enemyAI.transform.parent = transform;
    }

    public void PutAnotherEnemyTeam(){
        current_enemy_id++;
        if (current_enemy_id < GetEnemyCount)
            SpawnEnemy(current_enemy_id);
    }

    public int GetEnemyCount => _enemiesInTournament.Length;
}
