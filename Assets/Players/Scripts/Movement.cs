using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Particles Effects")]
    [SerializeField] GameObject _sparksParticlesFx;

    Joystick _joystick;
    Animator _myAnimator;
    Rigidbody _rigidBody;
    Character _playerStats;
    PlayerHealth _playerHealth;
    PowerEffects _playerPowerFx;
    private void Start() {
        _rigidBody = GetComponent<Rigidbody>();
        _myAnimator = GetComponent<Animator>();
        _joystick = FindObjectOfType<Joystick>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerPowerFx = GetComponent<PowerEffects>();
        _playerStats = _playerHealth.ReturnPlayerStats();
    }

    private void Update()
    {
        if (!_playerHealth.ItStoppedSpinning)
            SpinnerMovement();
    }

    void SpinnerMovement()
    {
        var realPlayerSpeed = _playerStats.speed * 100f;

        float movement_l_r = _joystick.Horizontal * realPlayerSpeed * Time.deltaTime;
        float movement_u_d = _joystick.Vertical * realPlayerSpeed * Time.deltaTime;

        _rigidBody.AddForce(movement_l_r, 0f, movement_u_d);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<EnemyAI>())
            _playerPowerFx.EnemyDebuffs(other.gameObject.GetComponent<EnemyAI>());
        var sparks = Instantiate(_sparksParticlesFx, other.GetContact(0).point, Quaternion.identity) as GameObject;
        sparks.GetComponent<AudioSource>().Play();
        Destroy(sparks, 0.5f);
    }

}
