using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpinHealthUI : MonoBehaviour
{
    EnemyAI enemyAI;
    Slider slider;

    private void OnEnable() {
        if (PlayerPrefs.GetInt("br_mode") != 1)
            GameObject.Find("BRSliderUI").SetActive(false);
        else
            GameObject.Find("EnemySpinningInfo").SetActive(false);
    }

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
