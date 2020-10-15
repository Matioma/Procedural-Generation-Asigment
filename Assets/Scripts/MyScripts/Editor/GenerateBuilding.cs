using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Buildings))]
public class GenerateBuilding : Editor
{
    public override void OnInspectorGUI()
    {
        var building = target as Buildings;

        base.OnInspectorGUI();
        if (GUILayout.Button("Generate")) {
            building.Trigger();
        }
    }
}
