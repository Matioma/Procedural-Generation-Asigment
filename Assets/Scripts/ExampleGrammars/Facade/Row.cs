using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo {
	public class Row : Shape {
		public int Number;
		public GameObject[] prefabs=null;
		public Vector3 direction;

		public void Initialize(int Number, GameObject[] pPrefabs, Vector3 dir=new Vector3()) {
			this.Number=Number;

			// All public reference types must be cloned, to avoid unexpected shared reference errors when 
			//  changing values in the inspector!:
			prefabs=(GameObject[])pPrefabs.Clone();

			if (dir.magnitude!=0) {
				direction=dir;
			} else {
				direction=new Vector3(0, 0, 1);
			}
		}

		protected override void Execute() {
			if (Number<=0)
				return;

			var param = Root.GetComponent<FacadeParameters>();
			int[] pattern = null;
			if (param!=null) {
				pattern=param.wallPattern;
			}

			for (int i=0;i<Number;i++) {            // spawn the prefabs
				// Choose a prefab index, either...
				int index = 0;
				if (pattern!=null && pattern.Length>0) { // ...given by the pattern from FacadeParameters, or ...
					index = pattern[i % pattern.Length] % prefabs.Length;
				} else { // ...(pseudo-)randomly chosen.
					index = RandomInt(prefabs.Length);
				}
				
				// Spawn the prefab, using i and the direction vector to determine the position:
				SpawnPrefab(prefabs[index],
					direction * (i - (Number-1)/2f)
				);
			}
		}
	}
}
