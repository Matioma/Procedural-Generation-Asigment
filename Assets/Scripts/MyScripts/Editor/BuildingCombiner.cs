using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(CombineMeshes))]
public class BuildingCombiner : Editor
{
    public override void OnInspectorGUI()
    {
        var meshCombiner = target as CombineMeshes;
        base.OnInspectorGUI();

        if (GUILayout.Button("CombineChildMeshes"))
        {
            meshCombiner.Optimize();
        }
    }
}
