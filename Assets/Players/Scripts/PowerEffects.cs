using UnityEngine;
using System.Collections;

public class PowerEffects : MonoBehaviour
{
    float speedStore;
    Character playerStats;
    PlayerHealth playerHealth;
    bool powerActive = false;
    bool isOnCooldown = false;
    ParticleSystem _particleFx;
    private void Start() {
        this.playerHealth = GetComponent<PlayerHealth>();
        this.playerStats = playerHealth.ReturnPlayerStats();
        this._particleFx = GetComponentInChildren<ParticleSystem>();
        Debug.Log(_particleFx);
    }

    public int ReturnPowerCooldown => playerStats.timeToCooldown;
    public bool IsPlayerOnCooldown => isOnCooldown;

    public void HabilityProcess()
    {
        if (!powerActive)
        {
            powerActive = true;
            speedStore = playerStats.speed;
            StartCoroutine(ActivateHabilty());
        }
    }

    IEnumerator CoolDownHability()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(playerStats.timeToCooldown);
        powerActive = false;
        isOnCooldown = false;
    }

    IEnumerator ActivateHabilty()
    {
        PlayerBuffs();
        _particleFx.Play();
        // SoundEffectManager(false);
        yield return new WaitForSeconds(playerStats.durationOfHability);
        // SoundEffectManager(true);
        playerStats.speed = speedStore;
        _particleFx.Stop();
        StartCoroutine(CoolDownHability());
    }

    public void PlayerBuffs()
    {
        switch (_particleFx.tag.ToLower())
        {
            case "lighting": // When the ability is lighting it will speed up the player.
                playerStats.speed = 6f;
                break;
            case "water": // It will regenerate portion of the health of the player over time. It will have shorter time and cooldown will be bigger.
                playerStats.timeToCooldown = 45;
                playerStats.durationOfHability = 5;
                int regenValue = Random.Range(10, 25);
                int spinRegenValue = Random.Range(50, 100);
                playerHealth.IncreaseHealth(regenValue);
                playerHealth.IncreaseSpinHealth(spinRegenValue);
                break;
            default:
                break;
        }
    }

    public void EnemyDebuffs(EnemyAI enemy)
    {
        if (powerActive)
        {
            switch (_particleFx.tag.ToLower())
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
}
