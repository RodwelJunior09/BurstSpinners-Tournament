using System;
using UnityEngine;
using UnityEngine.UI;

public class SpinHealthBar : MonoBehaviour
{
    Player player;
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        slider = this.GetComponent<Slider>();

        slider.maxValue = Convert.ToSingle(player.ReturnSpinHealth());
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Convert.ToSingle(player.ReturnSpinHealth());
    }
}
