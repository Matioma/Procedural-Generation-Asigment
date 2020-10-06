using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo {
	/// <summary>
	/// This is the component that holds the grid data, used by the MarchingSquares component.
	/// It is also responsible for mapping world positions to grid cell positions.
	/// Other scripts can call SetCell to modify the grid, before running the MarchingSquares algorithm.
	/// 
	/// You can try out different ways to initialize the grid here, in the InitializeGrid method.
	/// </summary>
	public class ValueGrid : MonoBehaviour {
		public int Width {
			get {
				return width;
			}
		}
		public int Depth {
			get {
				return depth;
			}
		}
		[SerializeField]
		int width = 10;
		[SerializeField]
		int depth = 10;

		public float cellSize = 1;
			   
		float[,] grid = null;

		private void Update() {
			if (Input.GetKeyDown(KeyCode.G)) {
				InitializeGrid();
				Debug.Log("ValueGrid: newly initialized. Press the BuildTrigger key to regenerate game objects");
			}
		}

		void InitializeGrid() {
			grid=new float[width, depth];

			// TODO: try out some interesting ways to initialize the grid here:
			float xOffset = Random.value;
			float yOffset = Random.value;

			float xRoad = Random.Range(0,width);
			float yRoad = Random.Range(0, depth);

			for (int i = 0; i<width; i++) {
				for (int j = 0; j<depth; j++) {

					if (j == xRoad) {
						grid[i, j] = xRoad;
					}
					if (i == yRoad)
					{
						grid[i, j] = yRoad;
					}
					// Initialize empty:
					//grid[i, j]=0; 

					// Perlin noise with random offset:
					//grid[i, j] = Mathf.Round(Mathf.PerlinNoise(i*0.1f + xOffset, j*0.1f + yOffset));
				}
			
			}
		}

		public bool GetRowCol(Vector3 worldPosition, out int row, out int col) {
			Vector3 localHit = transform.InverseTransformPoint(worldPosition);

			row = (int)Mathf.Round(localHit.x/cellSize);
			col = (int)Mathf.Round(localHit.z/cellSize);
			return InRange(row, col);
		}

		public bool InRange(int row, int col) {
			if (grid==null) {
				InitializeGrid();
			}
			return row>=0 && row<grid.GetLength(0) && col>=0 && col<grid.GetLength(1);
		}

		public void SetCell(int row, int col, float value) {
			if (grid==null) {
				InitializeGrid();
			}
			if (InRange(row,col)) {
				grid[row, col]=value;
			}
		}

		public float GetCell(int row, int col) {
			if (grid==null) {
				InitializeGrid();
			}
			if (InRange(row,col)) {
				return grid[row, col];
			}
			return 0;
		}

		public void SetCell(Vector3 worldPosition, float value) {
			if (GetRowCol(worldPosition, out int row, out int col)) {
				SetCell(row, col,value);
			}
		}

		public float GetCell(Vector3 worldPosition, float value) {
			if (GetRowCol(worldPosition, out int row, out int col)) {
				return GetCell(row, col);
			}
			return 0;
		}
	}
}