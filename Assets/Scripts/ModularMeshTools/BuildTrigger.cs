using UnityEngine;

namespace Demo {
	public class BuildTrigger : MonoBehaviour {
		public KeyCode BuildKey = KeyCode.Space;
		public bool BuildOnStart = false;

		Shape Root;
		RandomWithSeed random;

		void Start() {
			Root=GetComponent<Shape>();
			
			//if (BuildOnStart) {
			//	Build();
				
			//}
		}

		void Update() {
			if (Input.GetKeyDown(BuildKey)) {
				Build();
			}
		}

		public void Build() {
			GetComponent<Buildings>()?.Trigger();
            if (random != null)
            {
                random.ResetRandom();
            }
            if (Root != null)
            {
                Root.Generate();
            }
        }
	}
}