using System.Linq;
using UnityEngine;

public class BRTeamManager : MonoBehaviour
{
    HealthBar _healthBar;
    EnemyAI[] _allEnemiesAI;
    HabilityUI _habilityUI;
    PlayerHealth _playerHealth;
    SpinHealthBar _spinHealthBar;
    InicializeCamera _cameraComp;

    int team_member = 1;
    bool spawnEnemy = false;
    bool spawnedPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        _healthBar = FindObjectOfType<HealthBar>();
        _allEnemiesAI = FindObjectsOfType<EnemyAI>();
        _habilityUI = FindObjectOfType<HabilityUI>();
        _playerHealth = FindObjectOfType<PlayerHealth>(); 
        _spinHealthBar = FindObjectOfType<SpinHealthBar>();
        _cameraComp = FindObjectOfType<InicializeCamera>();
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
            if (enemy != null)
            {
                if (enemy.DoesEnemyStopped)
                {
                    enemy.GetComponentInParent<EnemySpawner>().PutAnotherEnemyTeam();
                    Destroy(enemy.gameObject);
                }
            }
        });
    }

    void PlayerChangeTeamMember(){
        if (_playerHealth != null)
        {
            if (_playerHealth.ItStoppedSpinning)
            {
                team_member += 1;
                _playerHealth.gameObject.GetComponentInParent<PlayerSpawner>().SpawnTeamPlayer(team_member);
                Destroy(_playerHealth.gameObject);
                RestorePlayerStats();
                RestoreEnemyTarget();
            }
        }
    }

    void RestoreEnemyTarget(){
        _allEnemiesAI.ToList().ForEach(enemy => {
            if (enemy != null)
            {
                enemy.SetPlayerEnemy();
            }
        });
    }
    
    void RestorePlayerStats(){
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _habilityUI.RestoreHability();
        _cameraComp.ChangeLookUpCamera();
        _healthBar.RestoreHealthBar();
        _spinHealthBar.RestoreSpinHealth();
    }
    
}
