using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static  class  OptimizeScene
{
    [MenuItem("Optimize Scene/MergeMeshes ")]
    public static void MergeMeshes() {
        Debug.Log("Merge");

        CombineMeshes[] combineMeshes = Resources.FindObjectsOfTypeAll<CombineMeshes>();
        foreach(var mesh in combineMeshes) {
            mesh.Optimize();
        }
        //Debug.Log(combineMeshes.Length);
    }
}
