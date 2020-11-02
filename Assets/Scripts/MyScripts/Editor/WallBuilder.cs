using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Wall))]
public class WallBuilder : Editor
{
    public override void OnInspectorGUI()
    {
        var wall = target as Wall;

        base.OnInspectorGUI();
        if (GUILayout.Button("Generate"))
        {
            wall.Generate();
        }
        if (GUILayout.Button("RandomSeed"))
        {
            wall.GetComponent<RandomGenerator>().seed = Random.Range(0, int.MaxValue);
            wall.Generate();
        }
    }
}
