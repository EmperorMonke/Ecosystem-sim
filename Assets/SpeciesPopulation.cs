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

    public TextMeshProUGUI populationText;

    private int bisonPopulation = 30;
    private int deerPopulation = 20;
    private int wolfPopulation = 10;
    private int rattlesnakePopulation = 5;
    private int falconPopulation = 15;

    public Vector3 spawnArea;

    private List<GameObject> deerList = new List<GameObject>();
    private List<GameObject> rattlesnakeList = new List<GameObject>();
    private List<GameObject> bisonList = new List<GameObject>();
    private List<GameObject> wolfList = new List<GameObject>();
    private List<GameObject> falconList = new List<GameObject>();

    private float moveSpeed = 3f;
    private float terrainSize = 500f;

    public float temperature = 0;
    public TextMeshProUGUI temperatureText;
    public int populationChange;

    void Start()
    {
        SpawnSpecies(bisonPrefab, bisonPopulation);
        SpawnSpecies(deerPrefab, deerPopulation);
        SpawnSpecies(wolfPrefab, wolfPopulation);
        SpawnSpecies(rattlesnakePrefab, rattlesnakePopulation);
        SpawnSpecies(falconPrefab, falconPopulation);

        RotateSpecies(deerList, new Vector3(-77.733f, 0f, 0f));
        RotateSpecies(rattlesnakeList, new Vector3(255.271f, 0f, 0f));
        RotateSpecies(bisonList, new Vector3(0f, 0f, 0f));
        RotateSpecies(wolfList, new Vector3(0f, 0f, 0f));
        RotateSpecies(falconList, new Vector3(0f, 0f, 0f));
    }

    void Update()
    {
        UpdatePopulationText();

        MoveOrganisms(deerList);
        MoveOrganisms(rattlesnakeList);
        MoveOrganisms(bisonList);
        MoveOrganisms(wolfList);
        MoveOrganisms(falconList);

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

    void SpawnSpecies(GameObject prefab, int population)
    {
        for (int i = 0; i < population; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-terrainSize, terrainSize),
                                            (float)9,
                                            Random.Range(-terrainSize, terrainSize));
            GameObject obj = Instantiate(prefab, randomPos, Quaternion.identity);
            if (prefab == deerPrefab)
            {
                deerList.Add(obj);
            }
            else if (prefab == rattlesnakePrefab)
            {
                rattlesnakeList.Add(obj);
            }
            else if (prefab == bisonPrefab)
            {
                bisonList.Add(obj);
            }
            else if (prefab == wolfPrefab)
            {
                wolfList.Add(obj);
            }
            else if (prefab == falconPrefab)
            {
                obj.transform.position = new Vector3(obj.transform.position.x, 57.2f, obj.transform.position.z);
                falconList.Add(obj);
            }
        }
    }

    void RotateSpecies(List<GameObject> speciesList, Vector3 rotation)
    {
        foreach (GameObject obj in speciesList)
        {   
            obj.transform.Rotate(rotation);
        }
    }

    void MoveOrganisms(List<GameObject> organismsList)
{
    foreach (GameObject obj in organismsList)
    {
        float edgeDistance = 10f; // distance from the edge at which a new direction is assigned

        // check if organism is close to the edge
        Vector3 pos = obj.transform.position;
        bool nearEdge = pos.x > terrainSize - edgeDistance ||
                        pos.x < -terrainSize + edgeDistance ||
                        pos.z > terrainSize - edgeDistance ||
                        pos.z < -terrainSize + edgeDistance;

        if (nearEdge)
        {
            // rotate organism 180 degrees and continue moving forward
            obj.transform.Rotate(Vector3.up, 180f);
            pos = obj.transform.position;
        }
        Vector3 forward;
        Vector3 targetPosition;
        if (obj.name == "Bison(Clone)") {
            forward = obj.transform.forward;
            targetPosition = pos - forward * moveSpeed * Time.deltaTime;
        }
        else if (obj.name == "12961_White-Tailed_Deer_v1_l2(Clone)" || obj.name == "10050_RattleSnake_v4_L3(Clone)") {
            forward = -obj.transform.right;
            targetPosition = pos + forward * moveSpeed * Time.deltaTime;
            targetPosition.y = 0; // maintain the same y position

        }
        else {
            forward = obj.transform.forward;
            targetPosition = pos + forward * moveSpeed * Time.deltaTime;
        }

        // check if new position is outside the terrain, and invert the direction if necessary
        if (targetPosition.x > terrainSize || targetPosition.x < -terrainSize ||
            targetPosition.z > terrainSize || targetPosition.z < -terrainSize)
        {
            obj.transform.Rotate(Vector3.up, 180f);
            forward = -forward;
            targetPosition = pos + forward * moveSpeed * Time.deltaTime;
        }

        obj.transform.position = targetPosition;

        // update original prefab position
        GameObject prefab = null;
        if (organismsList == deerList) prefab = deerPrefab;
        else if (organismsList == rattlesnakeList) prefab = rattlesnakePrefab;
        else if (organismsList == bisonList) prefab = bisonPrefab;
        else if (organismsList == wolfList) prefab = wolfPrefab;
        else if (organismsList == falconList) prefab = falconPrefab;

        if (prefab != null)
        {
            GameObject original = GameObject.Find(prefab.name + "(Clone)");
            if (original != null)
            {
                original.transform.position = obj.transform.position;
                original.transform.rotation = obj.transform.rotation;
            }
        }
    }
}

void UpdatePopulationText()
{
    populationText.text = "Bison: " + bisonPopulation+ "\n" +
                      "Deer: " + deerPopulation+ "\n" +
                      "Wolf: " + wolfPopulation + "\n" +
                      "Rattlesnake: " + rattlesnakePopulation+ "\n" +
                      "Falcon: " + falconPopulation;
}

 public void IncreaseTemperature()
    {
        temperature += 1;

        if (temperature == 1)
        {
            temperatureText.text = "High Temperature";
            bisonPopulation -= populationChange;
            wolfPopulation -= populationChange;
            rattlesnakePopulation += (populationChange+3);
            falconPopulation -= populationChange;
            
        }
        else if (temperature == 2)  
        {
            temperature = 1;
        }
        else
        {
            temperatureText.text = "Normal Temperature";
            bisonPopulation = 30;
            deerPopulation = 20;
            wolfPopulation = 10;
            rattlesnakePopulation = 5;
            falconPopulation = 15;
        }
    }

    public void DecreaseTemperature()
    {
        temperature -= 1;

        if (temperature == -1)
        {
            temperatureText.text = "Low Temperature";
            deerPopulation += populationChange;
            wolfPopulation += populationChange;
            rattlesnakePopulation -= populationChange;

        }
        else if (temperature == -2)
        {
            temperature = -1;
        }
        else
        {
            temperatureText.text = "Normal Temperature";
            bisonPopulation = 30;
            deerPopulation = 20;
            wolfPopulation = 10;
            rattlesnakePopulation = 5;
            falconPopulation = 15;
        }
    }
}