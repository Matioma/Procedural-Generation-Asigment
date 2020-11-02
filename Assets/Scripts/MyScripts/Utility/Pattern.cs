using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pattern
{
    [SerializeField]
    public int[] patern;

    int currentIndex = 0;
    public int GetValue()
    {
        Increment();
        return patern[currentIndex];
    }
    void Increment()
    {
        currentIndex = (currentIndex + 1) % patern.Length;

    }
    public int Length { get { return patern.Length; } }
}