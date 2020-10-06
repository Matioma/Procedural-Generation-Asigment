using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWithSeed : MonoBehaviour
{
	public int seed;
	static System.Random instance;

	/// <summary>
	/// Returns a random integer between 0 and maxValue-1 (inclusive).
	/// </summary>
	public int Next(int maxValue)
	{
		return Instance.Next(maxValue);
	}

	public System.Random Instance
	{
		get
		{
			if (instance == null)
			{
				ResetRandom();
			}
			return instance;
		}
	}

	public void ResetRandom()
	{
		instance = new System.Random(seed);
	}
}
