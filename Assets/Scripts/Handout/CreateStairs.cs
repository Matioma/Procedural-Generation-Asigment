using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Handout {
	public class CreateStairs : MonoBehaviour {
		public int numberOfSteps = 10;
		// The dimensions of a single step of the staircase:
		public float width=3;
		public float height=1;
		public float depth=1;

		MeshBuilder builder;

		void Start () {
			builder = new MeshBuilder ();
			CreateShape ();
			GetComponent<MeshFilter> ().mesh = builder.CreateMesh (true);
		}

		/// <summary>
		/// Creates a stairway shape in [builder].
		/// </summary>
		void CreateShape() {
			builder.Clear ();

			/**
			// V1: single step, hard coded:
			// bottom:
			int v1 = builder.AddVertex (new Vector3 (2, 0, 0), new Vector2 (1, 0));	
			int v2 = builder.AddVertex (new Vector3 (-2, 0, 0), new Vector2 (0, 0));
			// top front:
			int v3 = builder.AddVertex (new Vector3 (2, 1, 0), new Vector2 (1, 0.5f));	
			int v4 = builder.AddVertex (new Vector3 (-2, 1, 0), new Vector2 (0, 0.5f));
			// top back:
			int v5 = builder.AddVertex (new Vector3 (2, 1, 1), new Vector2 (0, 1));	
			int v6 = builder.AddVertex (new Vector3 (-2, 1, 1), new Vector2 (1, 1));

			builder.AddTriangle (v1, v2, v3);
			builder.AddTriangle (v2, v3, v4);
			builder.AddTriangle (v3, v4, v5);
			builder.AddTriangle (v4, v6, v5);

			/**
			// V2: single step, using the parameters from inspector:
			// bottom:
			int v1 = builder.AddVertex (new Vector3 (width / 2, 0, 0), new Vector2 (1, 0));	
			int v2 = builder.AddVertex (new Vector3 (-width / 2, 0, 0), new Vector2 (0, 0));
			// top front:
			int v3 = builder.AddVertex (new Vector3 (width / 2, height, 0), new Vector2 (1, 0.5f));	
			int v4 = builder.AddVertex (new Vector3 (-width / 2, height, 0), new Vector2 (0, 0.5f));
			// top back:
			int v5 = builder.AddVertex (new Vector3 (width / 2, height, depth), new Vector2 (0, 1));	
			int v6 = builder.AddVertex (new Vector3 (-width / 2, height, depth), new Vector2 (1, 1));

			builder.AddTriangle (v1, v2, v3);
			builder.AddTriangle (v2, v3, v4);
			builder.AddTriangle (v3, v4, v5);
			builder.AddTriangle (v4, v6, v5);


			/**/
			// V3, with for loop, using parameters:
			for (int i = 0; i < numberOfSteps; i++) {
				Vector3 offset = new Vector3 (0, height * i, depth * i); 

				// TODO: Fix the uvs:
				// bottom:
				int v1 = builder.AddVertex (offset + new Vector3 (width / 2, 0, 0), new Vector2 (1, 0));	
				int v2 = builder.AddVertex (offset + new Vector3 (-width / 2, 0, 0), new Vector2 (0, 0));
				// top front:
				int v3 = builder.AddVertex (offset + new Vector3 (width / 2, height, 0), new Vector2 (1, 0.5f));	
				int v4 = builder.AddVertex (offset + new Vector3 (-width / 2, height, 0), new Vector2 (0, 0.5f));


				// top front:
				int v5 = builder.AddVertex(offset + new Vector3(width / 2, height, 0), new Vector2(1, 0.5f));
				int v6 = builder.AddVertex(offset + new Vector3(-width / 2, height, 0), new Vector2(0, 0.5f));
				// top back:
				int v7 = builder.AddVertex (offset + new Vector3 (width / 2, height, depth), new Vector2 (0, 1));	
				int v8 = builder.AddVertex (offset + new Vector3 (-width / 2, height, depth), new Vector2 (1, 1));
			

				// TODO: Fix the winding order (everything clockwise):
				builder.AddTriangle (v1, v2, v3);
				builder.AddTriangle (v2, v4, v3);
				builder.AddTriangle (v5, v6, v7);
				builder.AddTriangle (v6, v8, v7);

				// TODO: Fix the normals by *not* reusing a single vertex in multiple triangles with different normals (solve it by creating more vertices at the same position)

				// TODO (slightly advanced): make the mesh solid by adding left, right and back side.
			}
			/**/
		}
		
	}
}