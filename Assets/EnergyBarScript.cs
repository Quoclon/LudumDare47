using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarScript : MonoBehaviour
{
    public Slider slider;

    public void SetEnergy(float currentEnergy)
    {
        slider.value = currentEnergy;
    }

    public void SetMaxEnergy(float energyMax)
    {
        slider.maxValue = energyMax;
    }
}
