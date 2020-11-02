
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(RoofTop))]
public class RoofGenerator : Editor
{

    public override void OnInspectorGUI()
    {
        var roofTop = target as RoofTop;

        base.OnInspectorGUI();
        if (GUILayout.Button("Generate"))
        {
            roofTop.Generate();
        }
        if (GUILayout.Button("RandomSeed"))
        {
            roofTop.GetComponent<RandomGenerator>().seed = Random.Range(0, int.MaxValue);
            roofTop.Generate();
        }
    }
}
