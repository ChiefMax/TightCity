using Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneratedObjectControl : MonoBehaviour
{

    public static GeneratedObjectControl instance;
    public List<GameObject> generatedObjects = new List<GameObject>();

    public GridSpawner gridSpawner;

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
        //Debug.Log("Prepare to generate");
        gridSpawner.Generate();
        //Debug.Log("Done generating");
    }
}
