using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo {
	public class Facade : Shape {
		public int HeightRemaining;
		public int Width;

		public void Initialize(int pHeightRemaining, int pWidth) {
			HeightRemaining=pHeightRemaining;
			Width=pWidth;
		}

		protected override void Execute() {
			if (HeightRemaining<=0)
				return;

			FacadeParameters param = Root.GetComponent<FacadeParameters>();

			Row wall = CreateSymbol<Row>("wall");
			wall.Initialize(Width, param.wallStyle);
			wall.Generate(param.buildDelay);

			Facade nextLayer = CreateSymbol<Facade>("facade", new Vector3(0, 1, 0));
			nextLayer.Initialize(HeightRemaining-1, Width);
			nextLayer.Generate(param.buildDelay);
		}
	}
}

