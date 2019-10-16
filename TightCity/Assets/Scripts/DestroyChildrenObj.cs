using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChildrenObj : MonoBehaviour
{
    public void RemoveChildren()
    {
        foreach (Transform child in transform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
    }
}
