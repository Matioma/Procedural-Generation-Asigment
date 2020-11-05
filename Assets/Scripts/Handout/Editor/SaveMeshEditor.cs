using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveMeshToOBJ))]
public class SaveMeshEditor : Editor {
	public override void OnInspectorGUI() {
		if (GUILayout.Button("Save Mesh")) {
			SaveMeshToOBJ saver = (SaveMeshToOBJ)target;
			saver.SaveMesh();
		}
		DrawDefaultInspector();
	}
}
