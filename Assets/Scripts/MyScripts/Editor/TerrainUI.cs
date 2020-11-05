using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainBuilder))]
public class TerrainUI : Editor
{
    public override void OnInspectorGUI()
    {
        var terrainBuilder = target as TerrainBuilder;
        base.OnInspectorGUI();

        if (GUILayout.Button("GenerateTerrain"))
        {
            terrainBuilder.Generate();
        }

        if (GUILayout.Button("Generate Random Seed"))
        {
            terrainBuilder.GetComponent<RandomGenerator>().seed = Random.Range(0, int.MaxValue);
            
            terrainBuilder.Generate();
        }
    }
}
