using System.Linq;
using UnityEngine;

public class BRTeamManager : MonoBehaviour
{
    LevelManager _lvlManager;
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
        _lvlManager = FindObjectOfType<LevelManager>();
        _playerHealth = FindObjectOfType<PlayerHealth>(); 
        _spinHealthBar = FindObjectOfType<SpinHealthBar>();
        _cameraComp = FindObjectOfType<InicializeCamera>();
    }

    private void Update() {
        if (PlayerPrefs.GetInt("br_mode") == 1)
        {
            EnemyChangeTeamMember();
            AllEnemiesDown();
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
                    _allEnemiesAI = FindObjectsOfType<EnemyAI>();
                }
            }
        });
    }

    void PlayerChangeTeamMember(){
        if (_playerHealth != null)
        {
            if (_playerHealth.ItStoppedSpinning && team_member <= 3)
            {
                team_member += 1;
                if (team_member > 3)
                    StartCoroutine(_lvlManager.PlayerLoseScreen());
                else {
                    _playerHealth.gameObject.GetComponentInParent<PlayerSpawner>().SpawnTeamPlayer(team_member);
                    Destroy(_playerHealth.gameObject);
                    RestorePlayerStats();
                    RestoreEnemyTarget();
                }
            }
        }
    }

    void AllEnemiesDown(){
        if (!FindObjectsOfType<EnemyAI>().Any())
            StartCoroutine(_lvlManager.LoadWinScreen());
    }

    void RestoreEnemyTarget(){
        _allEnemiesAI.ToList().ForEach(enemy => {
            if (enemy != null){
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
