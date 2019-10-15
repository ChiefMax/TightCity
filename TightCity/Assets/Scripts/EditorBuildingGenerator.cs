using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Demo;

public class EditorBuildingGenerator : Editor
{
    [CustomEditor(typeof(SampleScript))]
    public void OnSceneGUI()
    {
        BuildingPainter targetPainter = (BuildingPainter)target;

        if (GUILayout.Button("Generate"))
        {
            //targetPainter.CreateHouse(Random.onUnitSphere);
        }

    }

}
