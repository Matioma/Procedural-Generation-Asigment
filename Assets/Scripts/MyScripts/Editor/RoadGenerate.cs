using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[CustomEditor(typeof(Road))]
public class RoadGenerate : Editor
{
    public override void OnInspectorGUI()
    {
        var road = target as Road;

        base.OnInspectorGUI();
        if (GUILayout.Button("Generate"))
        {
            Debug.Log("Road Generated");
            road.Generate();
        }
    }
}
