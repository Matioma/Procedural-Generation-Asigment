using UnityEngine;
using System.IO;	// For file I/O
using System;		// For String formatting

public class SaveMeshToOBJ : MonoBehaviour {
	public string filename;
	
	public void SaveMesh() {
		var filter = GetComponent<MeshFilter>();
		if (filter!=null) {
			int index = 1;
			while (File.Exists(filename+index+".obj")) {
				index++;
			}
			string fullName = "Assets/" + filename + index + ".obj";
			SaveMeshToObj (GetComponent<MeshFilter> ().mesh, fullName);
			Debug.Log ("Saved mesh to " + fullName);
		}
	}

	/// <summary>
	/// Saves the mesh as an OBJ file.
	/// </summary>
	/// <param name="mesh">Mesh.</param>
	/// <param name="filename">Filename. Does not automatically set the extension.</param>
	static public void SaveMeshToObj(Mesh mesh, string filename) {
		StreamWriter writer = new StreamWriter (filename);

		//object name
		writer.WriteLine ("#Mesh\n");
		writer.WriteLine ("g mesh\n");

		//vertices
		for (int i=0; i<mesh.vertices.Length; i++) {
			float x = mesh.vertices[i].x;
			float y = mesh.vertices[i].y;
			float z = mesh.vertices[i].z;
			writer.WriteLine (String.Format ("v {0:F3} {1:F3} {2:F3}", x, y, z));
		}
		writer.WriteLine ("");

		//uv-set
		for (int i=0; i<mesh.uv.Length; i++) {
			float u = mesh.uv[i].x;
			float v = mesh.uv[i].y;
			writer.WriteLine (String.Format ("vt {0:F3} {1:F3}", u, v));
		}
		writer.WriteLine ("");

		//normals
		for (int i=0; i<mesh.normals.Length; i++) {
			float x = mesh.normals[i].x;
			float y = mesh.normals[i].y;
			float z = mesh.normals[i].z;
			writer.WriteLine (String.Format ("vn {0:F3} {1:F3} {2:F3}", x, y, z));
		}
		writer.WriteLine ("");

		//triangles
		for (int i=0; i<mesh.triangles.Length / 3; i++) {
			int v0 = mesh.triangles[i * 3 + 0]+1;
			int v1 = mesh.triangles[i * 3 + 1]+1;
			int v2 = mesh.triangles[i * 3 + 2]+1;
			writer.WriteLine(String.Format ("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}", v0, v1, v2));
		}

		writer.Close ();
	}
}
