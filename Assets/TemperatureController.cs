using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temperature : MonoBehaviour
public class TemperatureController : MonoBehaviour
{
    public float temperature;

    public void IncreaseTemperature()
    {
        temperature += 1;
    }

    public void DecreaseTemperature()
    {
        temperature -= 1;
    }
}
