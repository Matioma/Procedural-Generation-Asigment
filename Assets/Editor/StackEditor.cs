using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Demo {
	[CustomEditor(typeof(Stack))]
	public class StackEditor : Editor {
		public override void OnInspectorGUI() {
			Stack targetStack = (Stack)target;

			GUILayout.Label("Generated objects: "+targetStack.NumberOfGeneratedObjects);
			if (GUILayout.Button("Generate")) {
				targetStack.Generate(0.1f);
			}
			DrawDefaultInspector();
		}
	}
}
