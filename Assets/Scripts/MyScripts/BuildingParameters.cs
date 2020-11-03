using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingParameters
{
    [Header("Height Parameters")]
    [SerializeField, Range(1, 10)]
    public int minHeight = 1;
    [SerializeField, Range(1, 10)]
    public int maxHeight = 1;


    [Header("Width Parameters")]
    [SerializeField, Range(2, 10)]
    public int minWidth;
    [SerializeField, Range(2, 10)]
    public int maxWidth;

    [Header("Height Parameters")]
    [SerializeField, Range(2, 10)]
    public int minDepth;
    [SerializeField, Range(2, 10)]
    public int maxDepth;


    public BuildingParameters(BuildingParameters buildingParameters)
    {
        minHeight = buildingParameters.minHeight;
        maxHeight = buildingParameters.maxHeight;

        minWidth = buildingParameters.minWidth;
        maxWidth = buildingParameters.maxWidth;

        minDepth = buildingParameters.minDepth;
        maxDepth = buildingParameters.maxDepth;
    }
}
