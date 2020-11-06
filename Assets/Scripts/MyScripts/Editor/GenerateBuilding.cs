
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
            removeMesh();
            building.Trigger();
        }


        if (GUILayout.Button("RandomSeed"))
        {
            removeMesh();
            building.GetComponent<RandomGenerator>().seed = Random.Range(0, int.MaxValue);
            building.Trigger();
        }


        void removeMesh()
        {
            var render = building.GetComponent<MeshRenderer>();
            if (render)
            {
                DestroyImmediate(render);
            }
            var meshFillter = building.GetComponent<MeshFilter>();
            if (render)
            {
                DestroyImmediate(meshFillter);
            }
        }
    }


    
}
