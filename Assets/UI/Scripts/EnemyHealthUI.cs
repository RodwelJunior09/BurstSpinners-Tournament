using TMPro;
using UnityEngine;

public class EnemyHealthUI : MonoBehaviour
{
    EnemyAI enemyAI;
    TextMeshProUGUI textUI;

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = FindObjectOfType<EnemyAI>();
        textUI = this.GetComponent<TextMeshProUGUI>();

        textUI.text = $"HP: {enemyAI.ReturnEnemyHealth()}";
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAI.ReturnEnemyHealth() >= 0)
            textUI.text = $"HP: {enemyAI.ReturnEnemyHealth()}";
        else
            textUI.text = $"HP: {0}";
    }
}
