using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TemperatureController : MonoBehaviour
{
    public float temperature = 0;
    public TextMeshProUGUI temperatureText;
    public TextMeshProUGUI populationText;
    public float populationChange;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            IncreaseTemperature();
            populationChange = Random.Range(1, 9);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DecreaseTemperature();
            populationChange = Random.Range(1, 9);
        }
    }

    public void IncreaseTemperature()
    {
        temperature += 1;

        if (temperature == 1)
        {
            temperatureText.text = "High Temperature";
            
        }
        else if (temperature == 2)  
        {
            temperature = 1;
        }
        else
        {
            temperatureText.text = "Normal Temperature";
        }
    }

    public void DecreaseTemperature()
    {
        temperature -= 1;

        if (temperature == -1)
        {
            temperatureText.text = "Low Temperature";
        }
        else if (temperature == -2)
        {
            temperature = -1;
        }
        else
        {
            temperatureText.text = "Normal Temperature";
        }
    }
}