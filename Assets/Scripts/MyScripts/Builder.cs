using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(RandomGenerator))]
public class Builder : MonoBehaviour
{
    [HideInInspector]
    public  RandomGenerator random;

    public void SetSeed(int seed) {
        random.seed = seed;
    }

    virtual protected void Awake() {
        random = GetComponent<RandomGenerator>();
    }
    virtual public void Generate() {
        if (random == null) {
            random = GetComponent<RandomGenerator>();
        }
        random.ResetRandom();
    }
}
