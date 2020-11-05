using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOctahedron : MonoBehaviour
{
    void Start()
    {
		MeshBuilder builder = new MeshBuilder();
		/**
		// V1, bad winding:
		int v1 = builder.AddVertex(new Vector3(1, 0, 0));
		int v2 = builder.AddVertex(new Vector3(0, 0,-1));
		int v3 = builder.AddVertex(new Vector3(-1,0, 0));
		int v4 = builder.AddVertex(new Vector3(0, 0, 1));
		int v5 = builder.AddVertex(new Vector3(0, 1, 0));
		int v6 = builder.AddVertex(new Vector3(0,-1, 0));

		// top:
		builder.AddTriangle(v1, v2, v5);
		builder.AddTriangle(v2, v3, v5);
		builder.AddTriangle(v3, v4, v5);
		builder.AddTriangle(v4, v1, v5);

		// bottom:
		builder.AddTriangle(v1, v2, v6);
		builder.AddTriangle(v2, v3, v6);
		builder.AddTriangle(v3, v4, v6);
		builder.AddTriangle(v4, v1, v6);

		/**/
		// V2, correct winding:
		int v1 = builder.AddVertex(new Vector3(1, 0, 0));
		int v2 = builder.AddVertex(new Vector3(0, 0,-1));
		int v3 = builder.AddVertex(new Vector3(-1,0, 0));
		int v4 = builder.AddVertex(new Vector3(0, 0, 1));
		int v5 = builder.AddVertex(new Vector3(0, 1, 0));
		int v6 = builder.AddVertex(new Vector3(0,-1, 0));

		// top:
		builder.AddTriangle(v1, v2, v5);
		builder.AddTriangle(v2, v3, v5);
		builder.AddTriangle(v3, v4, v5);
		builder.AddTriangle(v4, v1, v5);

		// bottom:
		builder.AddTriangle(v1, v6, v2);
		builder.AddTriangle(v2, v6, v3);
		builder.AddTriangle(v3, v6, v4);
		builder.AddTriangle(v4, v6, v1);

		/**
		// V3, with uvs:
		int v1 = builder.AddVertex(new Vector3(1, 0, 0), new Vector2(0, 0));
		int v2 = builder.AddVertex(new Vector3(0, 0,-1), new Vector2(0, 1));
		int v3 = builder.AddVertex(new Vector3(-1,0, 0), new Vector2(1, 1));
		int v4 = builder.AddVertex(new Vector3(0, 0, 1), new Vector2(1, 0));
		int v5 = builder.AddVertex(new Vector3(0, 1, 0), new Vector2(0.5f, 0.5f));
		int v6 = builder.AddVertex(new Vector3(0,-1, 0), new Vector2(0.5f, 0.5f));

		// top:
		builder.AddTriangle(v1, v2, v5);
		builder.AddTriangle(v2, v3, v5);
		builder.AddTriangle(v3, v4, v5);
		builder.AddTriangle(v4, v1, v5);

		// bottom:
		builder.AddTriangle(v1, v6, v2);
		builder.AddTriangle(v2, v6, v3);
		builder.AddTriangle(v3, v6, v4);
		builder.AddTriangle(v4, v6, v1);

		/**/

		GetComponent<MeshFilter>().mesh=builder.CreateMesh();
	}
}
