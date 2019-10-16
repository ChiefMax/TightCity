using Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneratedObjectControl : MonoBehaviour
{

    public static GeneratedObjectControl instance;
    public List<GameObject> generatedObjects = new List<GameObject>();

    //public PerlinGenerator perlinGenerator;
    public GridSpawner gridSpawner;
    //public BuildingPainter painter;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddObject(GameObject objectToAdd)
    {
        generatedObjects.Add(objectToAdd);
    }


    public void Generate()
    {
        //perlinGenerator.Generate();
        Debug.Log("Prepare to generate");
        gridSpawner.Generate();
        //painter.CreateHouse();
        Debug.Log("Done generating");
    }

    public void Remove()
    {
        gridSpawner.RemoveAllClusters();
    }


    void ClearAllObjects()
    {
        for (int i = generatedObjects.Count - 1; i >= 0; i--)
        {
            generatedObjects[i].SetActive(false);
            generatedObjects.RemoveAt(i);
        }
    }
}
