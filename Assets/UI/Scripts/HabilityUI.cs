using System;
using UnityEngine;
using UnityEngine.UI;

public class HabilityUI : MonoBehaviour
{
    Text _textComponent;
    Button _activationBtn;
    PowerEffects _playerPowerFx;

    // Local variables
    float coolDownTime = 0f;

    private void OnEnable() {
        _textComponent = GetComponentInChildren<Text>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _activationBtn = GetComponent<Button>();
        _playerPowerFx = FindObjectOfType<PowerEffects>();
        _activationBtn.onClick.AddListener(() => ActivateHability());
        DissapearCooldownText();
    }

    void ActivateHability()
    {
        _playerPowerFx.HabilityProcess();
    }

    void Update()
    {
        if (_playerPowerFx.IsPlayerOnCooldown)
            AppearCoolDownText();
        else
            DissapearCooldownText();
    }

    void DissapearCooldownText()
    {
        _textComponent.gameObject.SetActive(false);
        coolDownTime = (float)_playerPowerFx.ReturnPowerCooldown;
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
