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

    [HideInInspector]
    public float streetLength;

    public float GetStreetLength() {
        return (endPosition - startPosition).magnitude;
    }


    public float intersectionsSize;


    public StreetParameters(StreetParameters param)
    {
        startPosition = new Vector3(param.startPosition.x, param.startPosition.y, param.startPosition.z);
        endPosition = new Vector3(param.endPosition.x, param.endPosition.y, param.endPosition.z);
        curvitureDepth = param.curvitureDepth;
        roadWidth = param.roadWidth;
        buildingDensity = new FloatRandomRange(param.buildingDensity);
        streetLength = param.streetLength;
        intersectionsSize = param.intersectionsSize;
    }
}
