using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Note: Unity's Mesh.CombineMeshes doesn't respect submeshes (and materials) well, 
// so here's a fully custom solution.
// Note that it identifies materials by name!
public class CombineMeshes : MonoBehaviour
{
	public KeyCode combineKey = KeyCode.Return;
	public KeyCode disableRendererKey = KeyCode.F9;
	public KeyCode destroyKey = KeyCode.F10;
	public bool destroyChildren = true;

	// Key = material name. Value = triangle list, with indices relative to the combined vertex list
	Dictionary<string, List<int>> materialTris;

	// Key = material name. Value = an instance of that material
	Dictionary<string, Material> materials;

	List<Vector3> verts;
	List<Vector2> uvs;
	List<Vector3> normals;
	// Possibly: Later add other things like (bi)tangents, colors, secondary uvs, ... too

    void Update()
    {
		
		if (Input.GetKeyDown(combineKey)) {
			Debug.Log("Remove");
			Initialize();
			AddChildMeshes(gameObject, Matrix4x4.identity);
			Combine();
			if (destroyChildren) {
				DestroyChildren();
			}
		}
		if (Input.GetKeyDown(destroyKey)) {
			Debug.Log("Destroying all child objects!");
			DestroyChildren();
		}
		if (Input.GetKeyDown(disableRendererKey)) {
			Debug.Log("Removing all mesh renderers!");
			DisableRenderers();
		}
    }

	void Initialize() {
		materialTris = new Dictionary<string, List<int>>();
		materials = new Dictionary<string, Material>();
		verts=new List<Vector3>();
		uvs=new List<Vector2>();
		normals=new List<Vector3>();
	}

	void AddChildMeshes(GameObject root, Matrix4x4 toLocal, bool recurse=true, bool doTransform=true) {
		//Debug.Log("Visiting "+root.name);
			   
		MeshFilter filter = root.GetComponent<MeshFilter>();
		MeshRenderer renderer = root.GetComponent<MeshRenderer>();

		if (filter!=null && renderer!=null && renderer.enabled) {
			//Debug.Log("Has filter + renderer");
			Mesh mesh = filter.sharedMesh;

			int vertexOffset = verts.Count;

			// Add vertex information to the combined mesh:
			if (!doTransform) {
				verts.AddRange(mesh.vertices);
				normals.AddRange(mesh.normals);
			} else {
				for (int vi=0;vi<mesh.vertices.Length;vi++) {
					verts.Add(toLocal.MultiplyPoint(mesh.vertices[vi]));
					normals.Add(toLocal.MultiplyVector(mesh.normals[vi]).normalized);
				}
			}
			uvs.AddRange(mesh.uv);
			// (Possibly: copy (bi)tangents etc. too)
			
			// For each submesh, copy triangles to the right new submesh:
			for (int sm=0;sm<mesh.subMeshCount;sm++) {
				Material origMat = renderer.materials[sm];

				if (!materials.ContainsKey(origMat.name)) { // Create a new material and submesh for the combined mesh
					materials[origMat.name]=origMat; // Maybe: Clone?
					materialTris[origMat.name]=new List<int>();
				}
				List<int> smTrisTarget = materialTris[origMat.name];

				int[] smTris = mesh.GetTriangles(sm);
				for (int i=0;i<smTris.Length;i++) {
					smTrisTarget.Add(smTris[i] + vertexOffset); // Don't forget to add the offset to the index!
				}
			}
		}

		// Visit all children recursively:
		if (recurse) {
			for (int ci = 0; ci<root.transform.childCount; ci++) {
				Transform child = root.transform.GetChild(ci);
				AddChildMeshes(
					child.gameObject,
					toLocal * Matrix4x4.TRS(child.localPosition, child.localRotation, child.localScale),		
					true,
					doTransform
				);
			}
		}
	}

	// After having visited all descendants and made all lists:
	// create the mesh, MeshFilter, and MeshRenderer, with the proper list of materials.
	void Combine() {
		if (materials.Count==0) return;

		if (verts.Count>65535) {
			Debug.Log("Too many vertices in mesh! Try to combine differently!");
			return;
		}
		Debug.Log(string.Format("Combining meshes for {0}. Creating new mesh with {1} vertices and {2} submeshes",
			name,verts.Count,materials.Count));

		// Do this after we know we can create a new mesh, but before adding the new renderer:
		DisableRenderers(); 

		Mesh newMesh = new Mesh();
		newMesh.vertices = verts.ToArray();
		newMesh.uv = uvs.ToArray();
		newMesh.normals = normals.ToArray();

		newMesh.subMeshCount=materials.Count;

		MeshFilter filter = gameObject.AddComponent<MeshFilter>();
		filter.mesh = newMesh;

		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();

		// Careful to work around get/set + reference types...:
		Material[] newMats = new Material[materials.Count];

		int index = 0;
		// Create materials and submeshes:
		foreach (string matName in materials.Keys) {
			newMats[index] = materials[matName];
			newMesh.SetTriangles(materialTris[matName].ToArray(), index);

			index++;
		}
		renderer.materials=newMats;

		// Maybe omit this, if not needed:
		newMesh.RecalculateTangents();
	}

	void DestroyChildren() {
		for (int ci = 0; ci<transform.childCount; ci++) {
			Destroy(transform.GetChild(ci).gameObject);
		}
	}

	void DisableRenderers(Transform root=null) {
		if (root==null) root=transform;
		MeshRenderer renderer = root.GetComponent<MeshRenderer>();
		if (renderer!=null) renderer.enabled=false;

		for (int ci = 0; ci<root.childCount; ci++) {
			DisableRenderers(root.GetChild(ci));
		}
	}
}
