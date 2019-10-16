﻿using Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{

    public int gridX = 4;
    public int gridZ = 4;
    public GameObject prefabToSpawn;
    public Vector3 gridOrigin = Vector3.zero;
    public float gridOffset = 2f;
    public bool generateOnEnable;
    public BuildingPainter builderPainter;
    public List<GameObject> objectsDestroy;


    void OnEnable()
    {
        if (generateOnEnable)
        {
            //Generate();
        }
    }

    public void Generate()
    {
        SpawnGrid();
        builderPainter.CreateHouse();
    }


    void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                GameObject clone = Instantiate(prefabToSpawn, 
                    transform.position + gridOrigin + new Vector3(gridOffset * x, 0, gridOffset * z), transform.rotation);
                clone.transform.SetParent(this.transform);
                builderPainter = clone.GetComponent<BuildingPainter>();
                if(builderPainter != null)
                  builderPainter.CreateHouse();
                AddToListForDestroy(clone);
            }
        }
    }

    public void AddToListForDestroy(GameObject gameObject)
    {
        objectsDestroy.Add(gameObject);
    }

    public void RemoveAllClusters()
    {
        foreach (var item in objectsDestroy)
        {
            objectsDestroy.Remove(item);
            DestroyImmediate(item);
        }
    }
}
