using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Player Atributtes")]
    public Character character;

    // Local Variables
    float speedStore;
    int playerRoundsWon = 0;
    bool isOnCooldown = false;

    // public bool stopSpinning = false;

    [SerializeField] bool powerActive = false; // Serialize for debugging propuses

    // Enemy Component
    EnemyAI enemy;

    // Component Variables
    Joystick joystick;
    Animator _myAnimator;
    Rigidbody _myRigidBody;
    LevelManager levelManager;
    AudioSource _powerSoundFx;
    ParticleSystem _powerEffect;
    AudioSource _deactivateSoundFX;

    // Start is called before the first frame update
    void Start()
    {
        InicializeComponents();
    }

    public int ReturnRoundWin => playerRoundsWon;

    public void IncreaseAmountOfWins()
    {
        playerRoundsWon++;
        if (PlayerPrefs.HasKey("rounds_won_by_player") && PlayerPrefs.GetInt("rounds_won_by_player") >= 3)
            PlayerPrefs.DeleteKey("rounds_won_by_player");
        else
        {
            var currentScore = PlayerPrefs.GetInt("rounds_won_by_player");
            PlayerPrefs.SetInt("rounds_won_by_player", currentScore + playerRoundsWon);
        }
    }

    private void InicializeComponents()
    {
        enemy = FindObjectOfType<EnemyAI>();
        _myAnimator = GetComponent<Animator>();
        joystick = FindObjectOfType<Joystick>();
        _myRigidBody = GetComponent<Rigidbody>();
        levelManager = FindObjectOfType<LevelManager>();
        _deactivateSoundFX = FindObjectOfType<AudioSource>();
        _powerEffect = GetComponentInChildren<ParticleSystem>();
        _powerSoundFx = _powerEffect.GetComponent<AudioSource>();
        _powerEffect.Stop();
        if (!PlayerPrefs.HasKey("rounds_won_by_player"))
            PlayerPrefs.SetInt("rounds_won_by_player", 0);
    }
}
