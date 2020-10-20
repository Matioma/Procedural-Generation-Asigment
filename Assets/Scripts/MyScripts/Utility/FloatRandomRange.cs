using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatRandomRange
{
    [SerializeField, Range(0, 20)]
    public float minValue = 1;
    [SerializeField, Range(0, 20)]
    public float maxValue = 1;


    public FloatRandomRange() { 
    
    }
    public FloatRandomRange(FloatRandomRange randomRange) {
        minValue = randomRange.minValue;
        maxValue = randomRange.maxValue;
    }

    public void Validate()
    {
        if (minValue > maxValue)
        {
            minValue = maxValue;
        }
    }
}
