using System;
using UnityEngine;
using UnityEngine.UI;

public class SpinHealthBar : MonoBehaviour
{
    Slider slider;
    PlayerHealth healthPlayer;

    // Start is called before the first frame update
    void Start()
    {
        slider = this.GetComponent<Slider>();
        healthPlayer = FindObjectOfType<PlayerHealth>();

        slider.maxValue = Convert.ToSingle(healthPlayer.ReturnSpinHealth());
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Convert.ToSingle(healthPlayer.ReturnSpinHealth());
    }

    public void RestoreSpinHealth(){
        healthPlayer = FindObjectOfType<PlayerHealth>();
    }
}
