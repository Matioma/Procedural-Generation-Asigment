using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Builder))]
public class BuilderGenerateButtons : Editor
{
    public override void OnInspectorGUI()
    {
        var builderComponent = target as Builder;

        base.OnInspectorGUI();
        if (GUILayout.Button("Generate"))
        {
            builderComponent.Generate();
        }
        if (GUILayout.Button("RandomSeed"))
        {
            builderComponent.SetSeed(Random.Range(0, int.MaxValue));
            builderComponent.Generate();
        }
    }
}
