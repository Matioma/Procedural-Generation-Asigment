using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IntRandomRange
{
    [SerializeField, Range(0, 100)]
    int minValue = 1;
    [SerializeField, Range(0, 100)]
    int maxValue = 1;
    public void Validate()
    {
        if (minValue > maxValue)
        {
            minValue = maxValue;
        }
    }
}
