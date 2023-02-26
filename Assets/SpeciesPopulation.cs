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

        Vector3 forward = obj.transform.forward;
        Vector3 targetPosition = pos + forward * moveSpeed * Time.deltaTime;

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
    populationText.text = "Bison: " + bisonList.Count + "\n" +
                          "Deer: " + deerList.Count + "\n" +
                          "Wolf: " + wolfList.Count + "\n" +
                          "Rattlesnake: " + rattlesnakeList.Count + "\n" +
                          "Falcon: " + falconList.Count;
}
}