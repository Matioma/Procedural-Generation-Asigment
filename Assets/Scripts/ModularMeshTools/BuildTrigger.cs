using UnityEngine;

namespace Demo {
	public class BuildTrigger : MonoBehaviour {
		public KeyCode BuildKey = KeyCode.Space;
		public bool BuildOnStart = false;

		Shape Root;
		RandomWithSeed random;

		void Start() {
			Root=GetComponent<Shape>();
			random = GetComponent<RandomWithSeed>();
			if (BuildOnStart) {
				Build();
			}
		}

		void Update() {
			if (Input.GetKeyDown(BuildKey)) {
				Build();

				GetComponent<Buildings>()?.resetFloorPlan();
			}
		}

		void Build() {
			if (random != null) {
				random.ResetRandom();
			}
			if (Root!=null) {
				Root.Generate();
			}
		}
	}
}