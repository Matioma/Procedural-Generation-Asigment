using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerainGenerator : MonoBehaviour
{
    [SerializeField]
    int height = 20;
    [SerializeField]
    int width=256;
    [SerializeField]
    int depth=256;

    [SerializeField, Range(0, 20)]
    float scale = 10.0f;

    [SerializeField]
    float offsetX;
    [SerializeField]
    float offsetY;


    private void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData data) {
        data.heightmapResolution = width + 1;
        data.size = new Vector3(width, height, depth);

        data.SetHeights(0, 0, GenerateHeights());
            

        return data;
    }


    float[,] GenerateHeights() {
        float[,] heights = new float[width, depth];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                heights[x, y] = CalculateHeight(x,y);
            }
        
        }

        return heights;
    }

    private float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;

       // float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
