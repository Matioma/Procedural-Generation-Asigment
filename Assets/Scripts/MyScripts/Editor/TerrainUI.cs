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
    }
}
