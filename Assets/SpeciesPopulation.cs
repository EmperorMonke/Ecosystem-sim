using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeciesPopulation : MonoBehaviour
{
    public GameObject bisonPrefab;
    public GameObject deerPrefab;
    public GameObject wolfPrefab;
    public GameObject rattlesnakePrefab;
    public GameObject falconPrefab;

    public TextMeshProUGUI bisonPopulationText;
    public TextMeshProUGUI deerPopulationText;
    public TextMeshProUGUI wolfPopulationText;
    public TextMeshProUGUI rattlesnakePopulationText;
    public TextMeshProUGUI falconPopulationText;

    private int bisonPopulation = 30;
    private int deerPopulation = 20;
    private int wolfPopulation = 10;
    private int rattlesnakePopulation = 5;
    private int falconPopulation = 15;

    public Vector3 spawnArea;

    private List<GameObject> deerList = new List<GameObject>();
    private List<GameObject> rattlesnakeList = new List<GameObject>();

    void Start()
    {
        SpawnSpecies(bisonPrefab, bisonPopulation);
        SpawnSpecies(deerPrefab, deerPopulation);
        SpawnSpecies(wolfPrefab, wolfPopulation);
        SpawnSpecies(rattlesnakePrefab, rattlesnakePopulation);
        SpawnSpecies(falconPrefab, falconPopulation);

        RotateSpecies(deerList, new Vector3(-77.733f, 0f, 0f));
        RotateSpecies(rattlesnakeList, new Vector3(255.271f, 0f, 0f));
    }

    void Update()
    {
        UpdateLabel(bisonPopulationText, bisonPopulation);
        UpdateLabel(deerPopulationText, deerList.Count);
        UpdateLabel(wolfPopulationText, wolfPopulation);
        UpdateLabel(rattlesnakePopulationText, rattlesnakeList.Count);
        UpdateLabel(falconPopulationText, falconPopulation);
    }

    void SpawnSpecies(GameObject prefab, int population)
    {
        for (int i = 0; i < population; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 
                                            (float)9,  
                                            Random.Range(-spawnArea.z, spawnArea.z));
            GameObject obj = Instantiate(prefab, randomPos, Quaternion.identity);
            if (prefab == deerPrefab)
            {
                deerList.Add(obj);
            }
            else if (prefab == rattlesnakePrefab)
            {
                rattlesnakeList.Add(obj);
            }
        }
    }

    void UpdateLabel(TextMeshProUGUI text, int population)
    {
        text.text = text.name + ": " + population;
    }

    void RotateSpecies(List<GameObject> speciesList, Vector3 rotation)
    {
        foreach (GameObject obj in speciesList)
        {
            obj.transform.Rotate(rotation);
        }
    }
}