using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpinHealthUI : MonoBehaviour
{
    EnemyAI enemyAI;
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = this.GetComponent<Slider>();
        enemyAI = FindObjectOfType<EnemyAI>();

        slider.maxValue = Convert.ToSingle(enemyAI.ReturnEnemySpinHealth());
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Convert.ToSingle(enemyAI.ReturnEnemySpinHealth());
    }
}
