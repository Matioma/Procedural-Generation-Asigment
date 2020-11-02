
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Buildings),true)]
public class GenerateBuilding : Editor
{
    public override void OnInspectorGUI()
    {
        var building = target as Buildings;

        base.OnInspectorGUI();
        if (GUILayout.Button("Generate")) {
            building.Trigger();
        }


        if (GUILayout.Button("RandomSeed"))
        {
            building.GetComponent<RandomGenerator>().seed = Random.Range(0, int.MaxValue);
            building.Trigger();
        }
    }
}
