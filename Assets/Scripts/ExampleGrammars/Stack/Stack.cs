using UnityEngine;

namespace Demo {
	public class Stack : Shape {
		public GameObject prefab;
		public int HeightRemaining;

		public void Initialize(GameObject pPrefab, int pHeightRemaining) {
			prefab=pPrefab;
			HeightRemaining=pHeightRemaining;
		}

		protected override void Execute() {
			// Spawn the (box) prefab as child of this game object:
			// (Optional parameters: localPosition, localRotation, alternative parent)
			GameObject box = SpawnPrefab(prefab);

			// Example: fat box:
			box.transform.localScale=new Vector3(1, 7, 1);

			if (HeightRemaining>0) {
				Stack newStack = null;


				/**
				//Example1

				newStack = CreateSymbol<Stack>("stack", new Vector3(0.0f, 1, 0));
				newStack.Initialize(prefab, HeightRemaining - 1);
				newStack.transform.localRotation = Quaternion.Euler(0, 0, -10);

				newStack.Generate(0.1f);

				/**
				//Example2

				newStack = CreateSymbol<Stack>("stack", new Vector3(0, 1, 0));
				newStack.Initialize(prefab,HeightRemaining-1);
				//newStack.transform.localScale = 
				newStack.transform.localRotation = Quaternion.Euler(0, 45, 0);
				newStack.Generate(0.1f);


				/**
				//Example 3


				newStack = CreateSymbol<Stack>("stack", new Vector3(-0.25f, 1.25f, 0));
				newStack.Initialize(prefab, HeightRemaining - 1);
				newStack.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
				newStack.transform.localRotation = Quaternion.Euler(0, 0, 45);
				
				newStack.Generate();

				newStack = CreateSymbol<Stack>("stack", new Vector3(0.25f,1.25f, 0));
				newStack.Initialize(prefab, HeightRemaining - 1);
				newStack.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
				newStack.transform.localRotation = Quaternion.Euler(0, 0, -45);
				newStack.Generate();


				/**
				//Example 4
				newStack = CreateSymbol<Stack>("stack", new Vector3(-0.25f, 1.25f, 0));
				newStack.Initialize(prefab, HeightRemaining - 1);
				newStack.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
				newStack.transform.localRotation = Quaternion.Euler(-45, 90, 0);
				
				newStack.Generate();

				newStack = CreateSymbol<Stack>("stack", new Vector3(0.25f,1.25f, 0));
				newStack.Initialize(prefab, HeightRemaining - 1);
				newStack.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
				newStack.transform.localRotation = Quaternion.Euler(45, 90, 0);
				newStack.Generate();

				/**/
				//Example 5
					newStack = CreateSymbol<Stack>("stack", new Vector3(-0.25f, 7 + 0.25f, 0));
				newStack.Initialize(prefab, HeightRemaining - 1);
				newStack.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
				newStack.transform.localRotation = Quaternion.Euler(-45, 90, 0);
				
				newStack.Generate();

				newStack = CreateSymbol<Stack>("stack", new Vector3(0.25f,7 +0.25f, 0));
				newStack.Initialize(prefab, HeightRemaining - 1);
				newStack.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
				newStack.transform.localRotation = Quaternion.Euler(45, 90, 0);
				newStack.Generate();

				/**
				// Simple stack:
				// Spawn a smaller stack on top of this:
				newStack = CreateSymbol<Stack>("stack", new Vector3(0, 1, 0));
				newStack.Initialize(prefab, HeightRemaining-1);
				// Generate it with a 0.1 second delay (when in play mode):
				newStack.Generate(0.1f); 

				/**
				// Scaling:
				// Every new stack gets a bit smaller:
				newStack = CreateSymbol<Stack>("stack", new Vector3(0, 1, 0));
				newStack.Initialize(prefab, HeightRemaining-1);
				newStack.transform.localScale=new Vector3(0.9f, 0.9f, 0.9f);
				newStack.Generate(0.1f); 

				/**
				// Rotation:
				// Every new stack rotates by 30 degrees around the y-axis:
				newStack = CreateSymbol<Stack>("stack", new Vector3(0, 1, 0));
				newStack.Initialize(prefab, HeightRemaining-1);
				newStack.transform.localRotation = Quaternion.Euler(0, 30, 0);
				newStack.Generate(0.1f); 

				/**
				// Rotation & scaling:
				// Every new stack rotates by 45 degrees around the z-axis, and becomes a bit smaller:
				newStack = CreateSymbol<Stack>("stack", new Vector3(-0.25f, 1.25f, 0));
				newStack.Initialize(prefab, HeightRemaining-1);
				newStack.transform.localRotation = Quaternion.Euler(0, 0, 45);
				newStack.transform.localScale=new Vector3(0.707f, 0.707f, 0.707f);
				newStack.Generate(0.1f); 				
			
				/**
				// Two smaller child stacks, spawned with an offset:
				// **** WARNING: don't do this with HeighRemaining values larger than about 8! ****
				//      (exponential growth breaks computers!)
				if (HeightRemaining>8) {
					HeightRemaining=8;
				}
				for (int i = 0; i<2; i++) {
					newStack = CreateSymbol<Stack>("stack", new Vector3(i-0.5f, 1, 0));
					newStack.Initialize(prefab, HeightRemaining-1);
					newStack.transform.localScale=new Vector3(0.5f, 0.5f, 0.5f);
					newStack.Generate(0.1f);
				}
				/**/
			}
		}
	}
}