using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    PlayerHealth healthPlayer;
    TextMeshProUGUI textUI;
    private void Start() {
        healthPlayer = FindObjectOfType<PlayerHealth>();
    
        textUI = this.GetComponent<TextMeshProUGUI>();

        textUI.text = $"HP: {healthPlayer.ReturnHealth()}";
    }

    private void Update() 
    {
        if (healthPlayer.ReturnHealth() >= 0)
            textUI.text = $"HP: {healthPlayer.ReturnHealth()}";
        else
            textUI.text = $"HP: {0}";
    }
}
