﻿using UnityEngine;

public class AutoUv : MonoBehaviour
{
	public Vector2 textureScaleFactor = new Vector2(1, 1);
	public bool UseWorldCoordinates;
	public bool AutoUpdate;
	public bool RecalculateTangents = true;

    void Update()
    {
        if ((transform.hasChanged && UseWorldCoordinates && AutoUpdate) || Input.GetKeyDown(KeyCode.F2)) {
			UpdateUvs();
			transform.hasChanged=false;
		}
    }

	public void UpdateUVs(Mesh mesh) {
		Debug.Log("Updating UVs");

		Vector2[] uv = mesh.uv;
		for (int i = 0; i<mesh.triangles.Length; i+=3) {
			int i1 = mesh.triangles[i];
			int i2 = mesh.triangles[i+1];
			int i3 = mesh.triangles[i+2];
			Vector3 v1 = mesh.vertices[i1];
			Vector3 v2 = mesh.vertices[i2];
			Vector3 v3 = mesh.vertices[i3];
			if (UseWorldCoordinates) {
				v1 = transform.TransformPoint(v1);
				v2 = transform.TransformPoint(v2);
				v3 = transform.TransformPoint(v3);
			}
			Vector3 tangent;
			Vector3 biTangent;
			// TODO: Take vertices that are part of multiple triangles + slight mesh warping into account.
			//  Possible solution:
			//   Store the computed tangent & bitangent for each vertex.
			//   If those have already been computed for at least one of the triangle vertices, assign those to all triangle vertices, instead of recomputing.
			ComputeTangents(v1, v2, v3, out tangent, out biTangent);
			ComputeTriangleUVs(v1, v2, v3, ref uv[i1], ref uv[i2], ref uv[i3], tangent, biTangent);
		}
		mesh.uv=uv;
		if (RecalculateTangents) {
			mesh.RecalculateTangents();
		}
	}

	public void UpdateUvs() {
		// Clone the shared mesh manually, to prevent the "leaking meshes" error:
		Mesh origMesh = GetComponent<MeshFilter>().sharedMesh; 
		Mesh mesh = (Mesh)Instantiate(origMesh);

		UpdateUVs(mesh);

		GetComponent<MeshFilter>().mesh=mesh;
	}

	void ComputeTangents(Vector3 v1, Vector3 v2, Vector3 v3, out Vector3 tangent, out Vector3 biTangent) {
		// Compute a normal for this triangle, using cross product (see 3D Math):
		Vector3 normal = Vector3.Cross(v2-v1, v3-v2);
		// If the triangle has almost zero area, the normal will be small as well, but then the uvs won't matter much anyway:
		if (normal.magnitude<=0.000001) {
			tangent=new Vector3();
			biTangent=new Vector3();
			return;
		}
		normal.Normalize();
		Vector3 up = Mathf.Abs(normal.y)>0.98 ? 
			Vector3.right : Vector3.up;
		// Use the cross product again to compute two unit vectors that are perpendicular to the normal, and to each other:
		tangent = Vector3.Cross(up, normal).normalized;
		biTangent = Vector3.Cross(normal, tangent);
	}

	void ComputeTriangleUVs(Vector3 v1, Vector3 v2, Vector3 v3, ref Vector2 uv1, ref Vector2 uv2, ref Vector2 uv3, Vector3 tangent, Vector3 biTangent) {
		// Use the dot product onto unit vectors (the tangent and bitangent) for scalar projection. 
		// This gives the coordinates of each of the three points v1, v2 and v3, relative to the vector basis given by tangent & bitangent.
		// (See the 3D Math course for more details!)
		// Those coordinates will be used as the uvs.
		uv1 = new Vector2(Vector3.Dot(v1, tangent), Vector3.Dot(v1, biTangent)) / textureScaleFactor;
		uv2 = new Vector2(Vector3.Dot(v2, tangent), Vector3.Dot(v2, biTangent)) / textureScaleFactor;
		uv3 = new Vector2(Vector3.Dot(v3, tangent), Vector3.Dot(v3, biTangent)) / textureScaleFactor;
	}
}
