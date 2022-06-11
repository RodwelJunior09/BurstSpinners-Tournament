using System.Linq;
using UnityEngine;

public class BRTeamManager : MonoBehaviour
{
    EnemyAI[] _allEnemiesAI;
    PlayerHealth _playerHealth;

    int team_member = 0;
    bool spawnEnemy = false;
    bool spawnedPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        _allEnemiesAI = FindObjectsOfType<EnemyAI>();
        _playerHealth = FindObjectOfType<PlayerHealth>(); 
    }

    private void Update() {
        if (PlayerPrefs.GetInt("br_mode") == 1)
        {
            EnemyChangeTeamMember();
            PlayerChangeTeamMember();
        }
    }

    void EnemyChangeTeamMember(){
        _allEnemiesAI.ToList().ForEach(enemy => {
            if (enemy.DoesEnemyStopped)
            {
                enemy.GetComponentInParent<EnemySpawner>().PutAnotherEnemyTeam();
                Destroy(enemy.gameObject);
            }
        });
    }

    void PlayerChangeTeamMember(){
        if (_playerHealth.ItStoppedSpinning)
        {
            team_member += 1;
            _playerHealth.gameObject.GetComponentInParent<PlayerSpawner>().SpawnTeamPlayer(team_member);
            Destroy(_playerHealth.gameObject);
        }
    }
    
}
