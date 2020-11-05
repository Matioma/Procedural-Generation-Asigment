using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizedCity : MonoBehaviour
{

    public void MergeBuildings() {
       Transform[] children= GetComponentsInChildren<Transform>();
        Debug.Log("WTF");
        Debug.Log(children.Length);
        //foreach (Transform child in children) {
        //    child.GetComponent<CombineMeshes>()?.Optimize();
        //}

    }
}
