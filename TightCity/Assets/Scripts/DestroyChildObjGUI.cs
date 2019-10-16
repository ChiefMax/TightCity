using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DestroyChildrenObj))]
public class DestroyChildObjGUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DestroyChildrenObj childrenObj = (DestroyChildrenObj)target;
        if (GUILayout.Button("Remove children"))
        {
            childrenObj.RemoveChildren();
        }
    }
}
