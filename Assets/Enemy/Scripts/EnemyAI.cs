using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] int enemyHealth = 500;
    [SerializeField] GameObject _particlesEffects;
    [SerializeField] double enemySpinHealth = 1000;

    // Local variables
    float defaultSpeed;
    float defaultDistance;
    bool isStopped = false;
    int habilityTimeUse = 10;
    int amountOfRoundWon = 0;
    bool isOnCoolDown = false;
    float habilityCooldown = 30;
    bool habilityActivated = false;

    // Local components variables
    Camera camera;
    Player player;
    Animator animator;
    NavMeshAgent agent;
    AudioSource _clashSoundFx;
    LevelManager levelManager;
    AudioSource _powerSoundFx;
    Transform playerTransform;
    ParticleSystem _powerEffect;
    AudioSource _deactivateSoundFx;

    private void Awake()
    {
        this.camera = FindObjectOfType<Camera>();
        this.agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        this.animator = GetComponent<Animator>();
        this.player = FindObjectOfType<Player>();
        this.playerTransform = player.transform;
        this._clashSoundFx = GetComponent<AudioSource>();
        this.levelManager = FindObjectOfType<LevelManager>();
        this._deactivateSoundFx = FindObjectOfType<AudioSource>();
        this._powerEffect = GetComponentInChildren<ParticleSystem>();
        this._powerSoundFx = _powerEffect.GetComponent<AudioSource>();
        this.defaultSpeed = agent.speed;
        this.defaultDistance = agent.stoppingDistance;
        if (!PlayerPrefs.HasKey("rounds_won_by_enemy"))
            PlayerPrefs.SetInt("rounds_won_by_enemy", 0);
    }

    private void Update()
    {
        RandomPowerAttack();
        DefensiveTake();
        EnemyDestroyed();
        EnemyHasNoPower();
    }

    private void FixedUpdate()
    {
        agent.SetDestination(playerTransform.position);
    }

    public void RandomPowerAttack()
    {
        var randomNumber = Random.Range(1, 100);
        if (randomNumber % 25 == 0 && !isOnCoolDown)
            HabilityProcess();
    }

    private void DefensiveTake()
    {
        bool haveLoweredStats = enemyHealth < 250 || enemySpinHealth < 500;
        if (haveLoweredStats && !habilityActivated)
            agent.stoppingDistance = 2;
    }
    
    public void IncreaseRoundWon()
    {
        amountOfRoundWon++;
        if (PlayerPrefs.HasKey("rounds_won_by_enemy") && PlayerPrefs.GetInt("rounds_won_by_enemy") >= 3)
            PlayerPrefs.DeleteKey("rounds_won_by_enemy");
        else
        {
            var currentScore = PlayerPrefs.GetInt("rounds_won_by_enemy");
            PlayerPrefs.SetInt("rounds_won_by_enemy", currentScore + amountOfRoundWon);
        }
    }

    public int ReturnRoundsWon() => amountOfRoundWon;
    public int ReturnEnemyHealth() => enemyHealth;
    public double ReturnEnemySpinHealth() => enemySpinHealth;
    public void DecreaseEnemyHealth(int damageDeal){
        enemyHealth -= damageDeal;
    }

    public void DecreaseEnemySpinHealth(int damageDeal){
        enemySpinHealth -= damageDeal;
    }

    public void HabilityProcess()
    {
        if (!habilityActivated)
        {
            habilityActivated = true;
            if (agent.stoppingDistance == 2)
                agent.stoppingDistance = defaultDistance;
            StartCoroutine(ActivateHability());
        }
    }

    IEnumerator CoolDownHability()
    {
        isOnCoolDown = true;
        habilityActivated = false;
        yield return new WaitForSeconds(habilityCooldown);
        isOnCoolDown = false;
    }

    IEnumerator ActivateHability()
    {
        EnemyBuffs();
        SoundEffectsManager(false);
        yield return new WaitForSeconds(habilityTimeUse);
        SoundEffectsManager(true);
        agent.speed = defaultSpeed;
        _powerEffect.Stop();
        StartCoroutine(CoolDownHability());
    }

    void SoundEffectsManager(bool disableSoundFx)
    {
        if (disableSoundFx)
        {
            _powerSoundFx.mute = true;
            _deactivateSoundFx.Play();
        }
        else
        {
            _powerEffect.Play();
            _powerSoundFx.Play();
        }
    }

    private void EnemyDebuffs(Player player)
    {
        if (habilityActivated)
        {
            switch (_powerEffect.tag.ToLower())
            {
                case "fire": // When the ability is fire, the attack power will increase.
                    player.DecreasePlayerHealth(2);
                    player.DecreasePlayerSpinHealth(4);
                    break;
                case "lighting": // When the ability is lighting it will do critical strikes.
                    int criticalValue = Random.Range(5, 7);
                    player.DecreasePlayerHealth(1);
                    player.DecreasePlayerSpinHealth(criticalValue);
                    break;
                default:
                    player.DecreasePlayerHealth(1);
                    player.DecreasePlayerSpinHealth(2);
                    break;
            }
        }
        else
        {
            player.DecreasePlayerHealth(1);
            player.DecreasePlayerSpinHealth(2);
        }
    }

    private void EnemyBuffs()
    {
        switch (_powerEffect.tag.ToLower())
        {
            case "lighting": // When the ability is lighting it will speed up the player.
                defaultSpeed = agent.speed;
                agent.speed = 50f;
                break;
            case "water": // It will regenerate portion of the health. It will have shorter time and cooldown will be bigger.
                habilityCooldown = 45;
                habilityTimeUse = 5;
                int regenValue = Random.Range(10, 25);
                int spinRegenValue = Random.Range(50, 100);
                enemyHealth += regenValue;
                enemySpinHealth += spinRegenValue;
                break;
            default:
                break;
        }
    }

    public void EnemyDestroyed()
    {
        if (ReturnEnemyHealth() <= 0)
        {
            if (!isStopped)
                player.IncreaseAmountOfWins();
            isStopped = true;
            agent.isStopped = true;
            animator.SetBool("IsDestroyed", true);
            StartCoroutine(levelManager.TournamentManager());
        }
    }

    public void EnemyHasNoPower()
    {
        if (ReturnEnemySpinHealth() <= 0)
        {
            if (!isStopped)
                player.IncreaseAmountOfWins();
            isStopped = true;
            agent.isStopped = true;
            animator.SetBool("IsStopping", true);
            StartCoroutine(levelManager.TournamentManager());
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.GetComponent<Player>())
        {
            EnemyDebuffs(other.gameObject.GetComponent<Player>());
            _clashSoundFx.Play();
        }
        var sparks = Instantiate(_particlesEffects, other.GetContact(0).point, Quaternion.identity) as GameObject;
        Destroy(sparks, 1f);
    }
}
