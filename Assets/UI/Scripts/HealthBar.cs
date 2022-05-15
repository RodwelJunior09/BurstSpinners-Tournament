using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Player player;
    TextMeshProUGUI textUI;
    private void Start() {
        player = FindObjectOfType<Player>();
    
        textUI = this.GetComponent<TextMeshProUGUI>();

        textUI.text = $"HP: {player.ReturnHealth()}";
    }

    private void Update() 
    {
        if (player.ReturnHealth() >= 0)
            textUI.text = $"HP: {player.ReturnHealth()}";
        else
            textUI.text = $"HP: {0}";
    }
}
