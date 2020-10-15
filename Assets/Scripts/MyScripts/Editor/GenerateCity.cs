using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(CityBuilder))]
public class GenerateCity : Editor
{

    public override void OnInspectorGUI()
    {
        var building = target as CityBuilder;

        base.OnInspectorGUI();
        if (GUILayout.Button("Generate"))
        {
            building.Generate();
        }
    }
}
