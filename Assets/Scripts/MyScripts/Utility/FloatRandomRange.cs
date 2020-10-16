using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatRandomRange
{
    [SerializeField, Range(0, 100)]
    public float minValue = 1;
    [SerializeField, Range(0, 100)]
    public float maxValue = 1;
    public void Validate()
    {
        if (minValue > maxValue)
        {
            minValue = maxValue;
        }
    }
}
