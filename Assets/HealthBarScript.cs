using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;

    public void SetHealth(float currentHealth)
    {
        slider.value = currentHealth;
    }

    public void SetMaxHealth(float healthMax)
    {
        slider.maxValue = healthMax;
    }
}
