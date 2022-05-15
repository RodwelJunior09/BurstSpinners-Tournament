using UnityEngine;
using System.Collections;

// TO-DO:
// Game test the whole game.
// Deploy the game

public class Player : MonoBehaviour
{
    [Header("Player Atributtes")]
    public Character character;

    [Header("Particles Effects")]
    [SerializeField] GameObject _particlesEffects;

    // Character variables - For modification on game
    private int playerHealth;
    private double playerSpinHealth;

    private void Awake()
    {
        playerHealth = character.health;
        playerSpinHealth = character.spinHealth;
    }

    // Local Variables
    float speedStore;
    int playerRoundsWon = 0;
    bool isOnCooldown = false;
    public bool stopSpinning = false;

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

    // Update is called once per frame
    void Update()
    {
        StopSpinning();
        DestroyAnimation();
    }

    void FixedUpdate()
    {
        if (!stopSpinning)
            Movement();
    }
    public int ReturnRoundWin() => playerRoundsWon;
    public int ReturnHealth() => playerHealth;
    public double ReturnSpinHealth() => playerSpinHealth;
    public int ReturnPowerCooldown() => character.timeToCooldown;
    public bool IsPlayerOnCooldown() => isOnCooldown;
    public void DecreasePlayerHealth(int damageDeal) => playerHealth -= damageDeal;
    public void DecreasePlayerSpinHealth(int amount) => playerSpinHealth -= amount;
    public void StopSpinning()
    {
        if (playerSpinHealth <= 0)
        {
            if (!stopSpinning)
                enemy.IncreaseRoundWon();
            StopAllAnimations();
            _myAnimator.SetBool("IsStopping", stopSpinning);
            StartCoroutine(levelManager.TournamentManager());
        }
    }

    public void DestroyAnimation()
    {
        if (playerHealth <= 0)
        {
            if (!stopSpinning)
                enemy.IncreaseRoundWon();
            StopAllAnimations();
            _myAnimator.SetBool("IsDestroyed", true);
            StartCoroutine(levelManager.TournamentManager());
        }
    }

    public void HabilityProcess()
    {
        if (!powerActive)
        {
            powerActive = true;
            speedStore = character.speed;
            StartCoroutine(ActivateHabilty());
        }
    }

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

    private void StopAllAnimations()
    {
        stopSpinning = true;
        _powerEffect.Stop();
        _powerSoundFx.mute = true;
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

    IEnumerator CoolDownHability()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(character.timeToCooldown);
        powerActive = false;
        isOnCooldown = false;
    }

    IEnumerator ActivateHabilty()
    {
        PlayerBuffs();
        SoundEffectManager(false);
        yield return new WaitForSeconds(character.durationOfHability);
        SoundEffectManager(true);
        character.speed = speedStore;
        _powerEffect.Stop();
        StartCoroutine(CoolDownHability());
    }

    void SoundEffectManager(bool disableSoundFx)
    {
        if (disableSoundFx)
        {
            _powerSoundFx.mute = true;
            _deactivateSoundFX.Play();
        }
        else
        {
            _powerEffect.Play();
            _powerSoundFx.Play();
        }
    }

    void Movement()
    {
        var realPlayerSpeed = character.speed * 100f;

        float movement_l_r = joystick.Horizontal * realPlayerSpeed * Time.deltaTime;
        float movement_u_d = joystick.Vertical * realPlayerSpeed * Time.deltaTime;

        //float movement_l_r = Input.GetAxis("Horizontal") * realPlayerSpeed * Time.deltaTime;
        //float movement_u_d = Input.GetAxis("Vertical") * realPlayerSpeed * Time.deltaTime;

        _myRigidBody.AddForce(movement_l_r, 0f, movement_u_d);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<EnemyAI>())
            EnemyDebuffs(other.gameObject.GetComponent<EnemyAI>());
        var sparks = Instantiate(_particlesEffects, other.GetContact(0).point, Quaternion.identity) as GameObject;
        sparks.GetComponent<AudioSource>().Play();
        Destroy(sparks, 0.5f);
    }

    private void EnemyDebuffs(EnemyAI enemy)
    {
        if (powerActive)
        {
            switch (_powerEffect.tag.ToLower())
            {
                case "fire": // When the ability is fire, the attack power will increase.
                    enemy.DecreaseEnemyHealth(2);
                    enemy.DecreaseEnemySpinHealth(4);
                    break;
                case "lighting": // When the ability is lighting it will do critical strikes.
                    enemy.DecreaseEnemyHealth(1);
                    int criticalValue = Random.Range(5, 7);
                    enemy.DecreaseEnemySpinHealth(criticalValue);
                    break;
                default:
                    enemy.DecreaseEnemyHealth(1);
                    enemy.DecreaseEnemySpinHealth(2);
                    break;
            }
        }
        else
        {
            enemy.DecreaseEnemyHealth(1);
            enemy.DecreaseEnemySpinHealth(2);
        }
    }

    private void PlayerBuffs()
    {
        switch (_powerEffect.tag.ToLower())
        {
            case "lighting": // When the ability is lighting it will speed up the player.
                character.speed = 6f;
                break;
            case "water": // It will regenerate portion of the health of the player over time. It will have shorter time and cooldown will be bigger.
                character.timeToCooldown = 45;
                character.durationOfHability = 5;
                int regenValue = Random.Range(10, 25);
                int spinRegenValue = Random.Range(50, 100);
                playerHealth += regenValue;
                playerSpinHealth += spinRegenValue;
                break;
            default:
                break;
        }
    }
}
