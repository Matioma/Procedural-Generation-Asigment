using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBuilder : MonoBehaviour
{
	[SerializeField]
	int width;
	[SerializeField]
	int depth;

	[SerializeField]
	int resolutionX;
	[SerializeField]
	int resolutionZ;


	void Start()
	{
		MeshBuilder builder = new MeshBuilder();

		float xOffset =(float)width / resolutionX;
		float zOffset = (float)width / resolutionZ;

		for (int z = 0; z < resolutionZ - 1; z++) { 
			for (int x = 0; x < resolutionX; x++) {
				int v1 = builder.AddVertex(new Vector3(x * xOffset, 0, z * zOffset), new Vector2(xOffset*x, z * zOffset));
				int v2 = builder.AddVertex(new Vector3(x * xOffset +xOffset, 0, z * zOffset), new Vector2(x * xOffset + xOffset, z * zOffset));
				int v3 = builder.AddVertex(new Vector3(x * xOffset , 0, z * zOffset + zOffset), new Vector2(x * xOffset, z * zOffset + zOffset));
				int v4 = builder.AddVertex(new Vector3(x * xOffset + xOffset, 0, z * zOffset + zOffset),new Vector2(x * xOffset + xOffset, z * zOffset + zOffset));


				builder.AddTriangle(v1, v4, v2);
				builder.AddTriangle(v1, v3, v4);
			}
		}



		//int v1 = builder.AddVertex(new Vector3(0.5f, 0, 0), new Vector2(0, 0));
		//int v2 = builder.AddVertex(new Vector3(-0.5f, 0, 0), new Vector2(1, 0));
		//int v3 = builder.AddVertex(new Vector3(0.5f, 0, 1f), new Vector2(0, 1));
		//int v4 = builder.AddVertex(new Vector3(-0.5f, 0, 1f), new Vector2(1, 1));

		//builder.AddTriangle(v1, v2, v3);
		//builder.AddTriangle(v2, v4, v3);

		GetComponent<MeshFilter>().mesh.RecalculateNormals();

		GetComponent<MeshFilter>().mesh = builder.CreateMesh();
	}
}
