﻿using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Curve : MonoBehaviour {
	public List<Vector3> points;

	public void Apply() {
		MeshCreator creator = GetComponent<MeshCreator>();
		if (creator!=null) {
			creator.RecalculateMesh();
		}
	}
}

