using UnityEngine;
using UnityEngine.UI;

public class HabilityUI : MonoBehaviour
{
    Player player;
    Text _textComponent;
    Button buttonComponent;

    // Local variables
    float coolDownTime;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        buttonComponent = GetComponent<Button>();
        _textComponent = GetComponentInChildren<Text>();
        DissapearCooldownText();
        buttonComponent.onClick.AddListener(() => ActivateHability());
    }

    void ActivateHability()
    {
        player.HabilityProcess();
    }

    void Update()
    {
        if (player.IsPlayerOnCooldown())
            AppearCoolDownText();
        else
            DissapearCooldownText();
    }

    void DissapearCooldownText()
    {
        _textComponent.gameObject.SetActive(false);
        coolDownTime = player.ReturnPowerCooldown();
    }

    void AppearCoolDownText()
    {
        _textComponent.gameObject.SetActive(true);
        if (coolDownTime >= 0)
        {
            coolDownTime -= Time.deltaTime;
            _textComponent.text = $"{Mathf.FloorToInt(coolDownTime)}";
        }
    }
}
