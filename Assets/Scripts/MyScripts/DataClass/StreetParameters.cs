using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StreetParameters
{
    [HideInInspector]
    public Vector3 startPosition;
    [HideInInspector]
    public Vector3 endPosition;

    [SerializeField, Range(0, 150)]
    public float curvitureDepth = 20;


    [SerializeField, Range(0, 20)]
    public float roadWidth = 5;

    [SerializeField]
    public FloatRandomRange buildingDensity;


    public StreetParameters(StreetParameters param)
    {
        startPosition = param.startPosition;
        endPosition = param.endPosition;
        curvitureDepth = param.curvitureDepth;
        roadWidth = param.roadWidth;
        buildingDensity = new FloatRandomRange(param.buildingDensity);

    }
}
