using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.UI;
using TMPro;    

public class SpeciesPopulation : MonoBehaviour
{
    public GameObject bisonPrefab;
    public TextMeshProUGUI bisonPopulationGO;
    private int bisonPopulation = 30;
    public Vector3 spawnArea;

    void Start()
    {
        for (int i = 0; i < bisonPopulation; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 
                                            (float)9,  
                                            Random.Range(-spawnArea.z, spawnArea.z));
            Instantiate(bisonPrefab, randomPos, Quaternion.identity);
        }

        UpdateLabel();
    }

    void UpdateLabel()
    {
        bisonPopulationGO.text = "Bison Population: " + bisonPopulation;
    }
}
