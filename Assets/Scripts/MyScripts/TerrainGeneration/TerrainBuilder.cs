using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class TerrainBuilder : MonoBehaviour
{
	[SerializeField]
	int width =100;
	[SerializeField]
	int depth=100;

	[SerializeField]
	int height =100;

	[SerializeField]
	int resolutionX;
	[SerializeField]
	int resolutionZ;



	[Header("Terrain")]
	[SerializeField]
	Texture2D perlinNoiseTexture;

	[SerializeField]
	int tex_width = 256;
	[SerializeField]
	int tex_height = 256;

	[SerializeField]
	float scale = 20;

	[SerializeField]
	float offsetX = 100;
	[SerializeField]
	float offsetY = 100;


	void Start()
	{
		


		//GetComponent<Renderer>().material.mainTexture = perlinNoiseTexture;
	}


	public void Generate() {
		GenerateTexture();
		MeshBuilder builder = new MeshBuilder();
		builder.Clear();

		float xOffset = (float)width / resolutionX;
		float zOffset = (float)width / resolutionZ;

		for (int z = 0; z < resolutionZ - 1; z++)
		{
			for (int x = 0; x < resolutionX; x++)
			{
				float xCoordinate = x * xOffset;
				float zCoordinate = z * zOffset;

				int v1 = builder.AddVertex(new Vector3(xCoordinate, getPixelHeight(x, z), zCoordinate), new Vector2(xCoordinate, zCoordinate));
				int v2 = builder.AddVertex(new Vector3(xCoordinate + xOffset, getPixelHeight(x + 1, z), zCoordinate), new Vector2(xCoordinate + xOffset, zCoordinate));
				int v3 = builder.AddVertex(new Vector3(xCoordinate, getPixelHeight(x, z + 1), zCoordinate + zOffset), new Vector2(xCoordinate, zCoordinate + zOffset));
				int v4 = builder.AddVertex(new Vector3(xCoordinate + xOffset, getPixelHeight(x + 1, z + 1), zCoordinate + zOffset), new Vector2(xCoordinate + xOffset, zCoordinate + zOffset));

				builder.AddTriangle(v1, v4, v2);
				builder.AddTriangle(v1, v3, v4);
			}
		}
		GetComponent<MeshFilter>().mesh.RecalculateNormals();
		GetComponent<MeshFilter>().mesh = builder.CreateMesh();


	}


	float getPixelHeight(int x, int y) {
		Color pixelColor = perlinNoiseTexture.GetPixel(x, y);
		Debug.Log(pixelColor.r);
		float pixelHeight = pixelColor.r * height;

		

		return pixelHeight;
		
	}


	void GenerateTexture() {
		perlinNoiseTexture = new Texture2D(tex_width, tex_height);


		for (int x = 0; x < tex_width; x++) {
			for (int y = 0; y < tex_height; y++) {
				Color color = GetColor(x, y);
				perlinNoiseTexture.SetPixel(x, y, color);
			}
		}

		perlinNoiseTexture.Apply();
	}

	Color GetColor(int x, int y) {
		float xCoor = (float)x / tex_width *scale + offsetX;
		float yCoor = (float)y / tex_height *scale+ offsetY;

		float sample = Mathf.PerlinNoise(xCoor, yCoor);
		return new Color(sample, sample, sample);
	}


}
