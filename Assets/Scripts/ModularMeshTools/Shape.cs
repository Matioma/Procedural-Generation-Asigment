using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Demo {
	/// <summary>
	/// This is a superclass that you can use for any custom grammar.
	/// Note: To create grammars, it's not necessary to understand the implementation details here,
	///  you just need to know what the key methods do (see comments + lecture slides).
	/// Also: you probably shouldn't make changes in this class, unless you really know what you're doing.
	/// </summary>
	public abstract class Shape : MonoBehaviour {
		/// <summary>
		/// Returns the total number of generated game objects 
		/// Note: make sure that you only spawn game objects using the SpawnPrefab method, if you want
		///  them to be cleaned up properly.
		/// </summary>
		public int NumberOfGeneratedObjects {
			get {
				if (generatedObjects!=null)
					return generatedObjects.Count;
				else
					return 0;
			}
		}
		List<GameObject> generatedObjects = null;

		/// <summary>
		/// In any child game object / symbol of this grammar, Root will give a reference to the root game object 
		///  in the scene.
		/// This means that you can e.g. add custom global parameter components to the root game object, and 
		///  retrieve them anywhere using Root.GetComponent.
		/// </summary>
		public GameObject Root {
			get {
				if (root==null) {
					return gameObject;
				} else {
					return root;
				}
			}
		}
		GameObject root = null;

		/// <summary>
		/// A utility method for creating new grammar symbols, or in Unity terms: (child) game objects with a 
		///  Shape component.		
		/// For [T], pass the symbol name (a subclass of Shape). 
		/// [name] is the name of the resulting game object in the hierarchy.
		/// Optionally, you can pass in a (local) position and rotation for the new shape, and a parent transform.
		///  (By default, the parent is the game object of the current grammar symbol.)
		/// Returns the new Symbol (a.k.a. Shape component).
		/// </summary>
		protected T CreateSymbol<T>(string name, Vector3 localPosition=new Vector3(), Quaternion localRotation=new Quaternion(), Transform parent=null) where T : Shape {
			if (parent==null) {
				parent=transform; // default: add as child game object
			}
			GameObject newObj = new GameObject(name);
			newObj.transform.parent=parent;
			newObj.transform.localPosition=localPosition;
			newObj.transform.localRotation=localRotation;
			AddGenerated(newObj);
			T component = newObj.AddComponent<T>();
			component.root = Root;
			return component;
		}

		
		/// <summary>
		/// A utility method for spawning prefabs.
		/// Use this to spawn prefabs (=terminal symbols in the grammar) cleanly, such that they will be destroyed
		///  when calling DeleteGenerated.
		/// Optionally, you can pass in a position and rotation for the new shape, and a parent transform.
		///  (By default, the parent is the game object of the current grammar symbol.)
		/// Returns the generated game object.
		/// </summary>
		protected GameObject SpawnPrefab(GameObject prefab, Vector3 localPosition=new Vector3(), Quaternion localRotation=new Quaternion(), Transform parent=null) {
			if (parent==null) {
				parent=transform; // default: add as child game object
			}
			GameObject copy = Instantiate(prefab, parent);
			copy.transform.localPosition=localPosition;
			copy.transform.localRotation=localRotation;
			AddGenerated(copy);
			return copy;
		}

		/// <summary>
		/// Returns a random integer between 0 and MaxValue-1 (inclusive). 
		/// Uses The RandomGenerator attached to the root object, if that's there.
		/// If you extend the RandomGenerator class, you can get seeded pseudo random numbers this way.
		/// </summary>
		protected int RandomInt(int maxValue) {
			RandomGenerator rnd = Root.GetComponent<RandomGenerator>();
			if (rnd!=null) {
				return rnd.Next(maxValue);
			} else { // use Unity's random
				return Random.Range(0, maxValue);
			}
		}

		/// <summary>
		/// Returns a random float between 0 and 1. 
		/// Uses The RandomGenerator attached to the root object, if that's there.
		/// If you extend the RandomGenerator class, you can get seeded pseudo random numbers this way.
		/// </summary>
		protected float RandomFloat() {
			RandomGenerator rnd = Root.GetComponent<RandomGenerator>();
			if (rnd!=null) {
				return (float)(rnd.Rand.NextDouble());
			} else { // use Unity's random
				return Random.value;
			}
		}
				
		/// <summary>
		/// A utility method for selecting a random object from an array (e.g. a random prefab): 
		/// Uses The RandomGenerator attached to the root object, if that's there.
		/// If you extend the RandomGenerator class, you can get seeded pseudo random numbers this way.
		/// </summary>
		public T SelectRandom<T>(T[] objectArray) {
			return objectArray[RandomInt(objectArray.Length)];
		}

		/// <summary>
		/// Adds a game object to the list of generated game objects.
		/// Typically, if you implement your grammar properly (calling SpawnPrefab), you don't need to 
		///  call this method yourself.
		/// </summary>
		protected GameObject AddGenerated(GameObject newObject) {
			if (generatedObjects==null) {
				generatedObjects=new List<GameObject>();
			}
			generatedObjects.Add(newObject);
			return newObject;
		}

		/// <summary>
		/// Deletes all previously generated game objects, and runs the grammar again from this start symbol,
		///  with optionally a small delay.
		/// </summary>
		public void Generate(float delaySeconds = 0) {
			DeleteGenerated();
			if (delaySeconds==0 || !Application.isPlaying) {
				Execute();
			} else {
				StartCoroutine(DelayedExecute(delaySeconds));
			}
		}

		IEnumerator DelayedExecute(float delay) {
			yield return new WaitForSeconds(delay);
			Execute();
		}

		/// <summary>
		/// Deletes all previously generated game objects.
		/// </summary>
		public void DeleteGenerated() {
			if (generatedObjects==null)
				return;
			foreach (GameObject gen in generatedObjects) {
				if (gen==null)
					continue;
				// Delete recursively: (needed for when it's not a child of this game object)
				Shape shapeComp = gen.GetComponent<Shape>();
				if (shapeComp!=null)
					shapeComp.DeleteGenerated();

				DestroyImmediate(gen);
			}
			generatedObjects.Clear();
		}

		/// <summary>
		/// This method must be implemented in subclasses (=your own grammar symbols).
		/// This is where you apply grammar rules (possibly randomly selected).
		/// Typically, from this method you'll call 
		///   CreateSymbol to create new non-terminal symbols (=game objects with shape components), and
		///   SpawnPrefab to create terminal symbols (=game objects)
		/// </summary>
		protected abstract void Execute();		
	}
}