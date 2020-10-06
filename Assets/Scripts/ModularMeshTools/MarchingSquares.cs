using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo {
	/// <summary>
	/// When Executing this grammar, this component puts the given prefabs in a grid shape in the scene, determined by
	/// the values in the ValueGrid component, according to the marching squares algorithm.
	/// 
	/// You need to put the right kind of prefabs here (see the tooltip), and depending on their orientation, 
	/// you also need to put the right values in the prefabRotations array (see the tooltip). 
	/// 
	/// You can create all kinds of interesting shapes, by using different prefabs, and initializing the
	/// ValueGrid in different ways.
	/// 
	/// It is not necessary to edit or understand this script, though if you want to generalize marching squares
	/// (e.g. as shown in the lecture), you can.
	/// </summary>
	[RequireComponent(typeof(ValueGrid))]
	public class MarchingSquares : Shape {
		[Tooltip("Add 6 corner prefabs here, in this order: 0 corners / 1 corner / 2 adjacent corners / 2 opposite corners / 3 corners / 4 corners")]
		public GameObject[] cornerPrefabs;
		public GameObject debugPrefab;
		[Tooltip("Give 6 prefab rotations here, as a number between 0 and 3 (1 = 90 degrees, etc.)")]
		public int[] prefabRotations;

		ValueGrid grid;

		private void Awake() {
			grid=GetComponent<ValueGrid>();
			if (cornerPrefabs.Length<6 || prefabRotations.Length<6) {
				throw new System.Exception("Marching Squares: the Corner Prefabs and Prefab Rotations arrays need to have length at least 6!");
			} 
		}




		// Basically, in the next table we count the number of ones in the binary representation.
		// However when there are two ones, we distinguish two cases: adjacent or not.
		int[] prefabIndex = new int[] {
			0,	// 0 = 0000		case 0: no ones
			1,	// 1 = 0001		case 1: a single one
			1,	// 2 = 0010
			2,	// 3 = 0011		case 2: two adjacent ones
			1,	// 4 = 0100
			3,	// 5 = 0101		case 3: two nonadjacent ones
			2,	// 6 = 0110
			4,	// 7 = 0111		case 4: three ones
			1,	// 8 = 1000
			2,	// 9 = 1001		case 2 again: counting cyclically, these are adjacent!
			3,	// 10= 1010
			4,	// 11= 1011
			2,	// 12= 1100
			4,	// 13= 1101
			4,	// 14= 1110
			5   // 15= 1111		case 5: four ones
		};
		// The next table contains the rotation (which should be multiplied by 90 degrees)
		// compared to the default prefab rotation.	
		int[] rotationIndex = new int[] {
			0,	// 0 = 0000		
			0,	// 1 = 0001		
			1,	// 2 = 0010
			0,	// 3 = 0011
			2,	// 4 = 0100
			0,	// 5 = 0101		
			1,	// 6 = 0110
			3,	// 7 = 0111		
			3,	// 8 = 1000
			3,	// 9 = 1001		
			1,	// 10= 1010
			2,	// 11= 1011
			2,	// 12= 1100
			1,	// 13= 1101
			0,	// 14= 1110
			0	// 15= 1111	
		};

		public int GetBitMask(int i, int j) {
			int bitMask = 0;
			//Debug.Log("Checking cell "+i+","+j);
			for (int k = 0; k<4; k++) { // Loop over all corners, in cyclic order
										// The formula in the line below maps...
										//  k:	to:
										//  0	0,0
										//  1	1,0
										//  2	1,1
										//  3	0,1
				int x = i+(k/2);
				int y = j + ((k+1)/2)%2;
				if (grid.GetCell(x,y)>0) { // If the neighboring grid cell is filled...
					bitMask |= (1<<k); // ...set bit k of bitmask to 1.
				}
			}
			return bitMask;
		}

		public GameObject SpawnPrefab(int i, int j, int bitMask) {
			Vector3 spawnOffset = new Vector3(0.5f, 0, 0.5f);

			if (cornerPrefabs[prefabIndex[bitMask]]!=null) {
				return SpawnPrefab(
					cornerPrefabs[prefabIndex[bitMask]],
					(new Vector3(i, 0, j) + spawnOffset) * grid.cellSize,
					Quaternion.Euler(0, 90*(rotationIndex[bitMask] + prefabRotations[prefabIndex[bitMask]]), 0),
					transform
				);
			} else {
				return null;
			}
		}

		protected override void Execute() {
			// generate the game objects from the grid
			// Loop over all square cells between the grid sample points:
			// (If grid is n x m, the number of square cells is (n-1) x (m-1).)
			for (int i = 0; i<grid.Width-1; i++) {
				for (int j = 0; j<grid.Depth-1; j++) {
					int bitMask = GetBitMask(i, j);

					//Debug.Log("Position: "+i+","+j+": "+bitMask+" prefabIndex: "+prefabIndex[bitMask]+" rotationIndex: "+rotationIndex[bitMask]);

					// The result is a bitmask value between 0 (=0000) and 15 (=1111).					
					// Use the lookup tables to match this to a prefab and rotation:
					SpawnPrefab(i, j, bitMask);
				}
			}

			if (debugPrefab!=null) {
				// Spawn some game objects to show where the empty cells are:
				for (int i = 0; i<grid.Width; i++) {
					for (int j = 0; j<grid.Depth; j++) {
						if (grid.GetCell(i,j)<=0) {
							GameObject newObj = SpawnPrefab(debugPrefab,
								new Vector3(i, 0.1f, j) * grid.cellSize,
								Quaternion.identity,
								transform
							);
							newObj.transform.localScale=new Vector3(0.3f, 0.3f, 0.3f);
						}
					}
				}
			}
		}
	}
}