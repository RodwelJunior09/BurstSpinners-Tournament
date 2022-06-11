using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Atributtes")]
    public Character character;
    EnemyAI _enemyObj;
    Animator _myAnimator;
    LevelManager _lvlManager;

    // Character variables - For modification on game
    private int playerHealth;
    private double playerSpinHealth;
    public bool stopSpinning = false;

    private void Awake() {
        playerHealth = character.health;
        playerSpinHealth = character.spinHealth;
    }

    private void Start() {
        _enemyObj = FindObjectOfType<EnemyAI>();
        _myAnimator = GetComponent<Animator>();
        _lvlManager = FindObjectOfType<LevelManager>();
    }

    private void Update(){
        StopSpinning();
        SpinnerDestroyed();
    }

    public Character ReturnPlayerStats() => character;

    public void StopSpinning()
    {
        if (playerSpinHealth <= 0)
        {
            SetToStopSpin(true);
            _myAnimator.SetBool("IsStopping", stopSpinning);
            if (!stopSpinning && PlayerPrefs.GetInt("br_mode") != 1)
            {
                _enemyObj.IncreaseRoundWon();
                StartCoroutine(_lvlManager.TournamentManager());
            }
        }
    }

    public void SpinnerDestroyed()
    {
        if (playerHealth <= 0)
        {
            SetToStopSpin(true);
            _myAnimator.SetBool("IsDestroyed", true);
            if (!stopSpinning && PlayerPrefs.GetInt("br_mode") != 1)
            {
                _enemyObj.IncreaseRoundWon();
                StartCoroutine(_lvlManager.TournamentManager());
            }
        }
    }

    public void IncreaseHealth(int healthVal){
        playerHealth += healthVal;
    }

    public void IncreaseSpinHealth(int spinHealthVal) {
        playerSpinHealth += spinHealthVal;
    }

    public void SetToStopSpin(bool itStopped)
    {
        stopSpinning = itStopped;
    }

    public int ReturnHealth() => playerHealth;
    public bool ItStoppedSpinning => stopSpinning;
    public double ReturnSpinHealth() => playerSpinHealth;
    public void DecreasePlayerHealth(int damageDeal) => playerHealth -= damageDeal;
    public void DecreasePlayerSpinHealth(int amount) => playerSpinHealth -= amount;
}
