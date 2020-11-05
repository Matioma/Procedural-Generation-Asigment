using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OptimizedCity))]
public class OptimizeCityButton : Editor
{
    public override void OnInspectorGUI()
    {
        var optimizeCity = target as OptimizedCity;
        base.OnInspectorGUI();

        if (GUILayout.Button("Merge buildings in One mesh"))
        {
            optimizeCity.MergeBuildings();
        }
    }
}
