using Demo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;



[RequireComponent(typeof(RandomGenerator),typeof(RoadBuilder))]
public class CityBuilder : Builder
{

    [SerializeField]
    StreetParameters streetParameters;

    [SerializeField]
    int numberOfSplitsOfTheRoad;


    [SerializeField]
    public List<Vector3> CityShape;

    
    private void OnValidate()
    {
        //float maxLength = Mathf.Sqrt(width * width + depth * depth);

        //if (MinRoadLength >= maxLength)
        //{
        //    MinRoadLength = maxLength * 0.9f;
        //}

        //distanceBetweenBuildings.Validate();
    }

    private void Awake()
    {
        base.Awake();
        Generate();
    }

    public override void  Generate()
    {
        base.Generate();
        BuildCity();
    }

    void BuildCity() {
        RemoveChildren();
        buildRoad();
    }
    void RemoveChildren() {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void buildRoad() {
        if (CityShape.Count != 0)
        {
            for (int i = 0; i < CityShape.Count; i++)
            {
                streetParameters.startPosition = transform.position + CityShape[i];
                if (i < CityShape.Count-1)
                {
                    streetParameters.endPosition = transform.position + CityShape[i + 1];
                }
                else
                {
                    streetParameters.endPosition = transform.position + CityShape[0];
                }

                streetParameters.streetLength = (streetParameters.startPosition - streetParameters.endPosition).magnitude;
                GetComponent<RoadBuilder>()?.SpawnRoad(streetParameters, numberOfSplitsOfTheRoad);
            }
        }
        //else {
        //    streetParameters.startPosition = transform.position;
        //    streetParameters.endPosition = new Vector3(transform.position.x + width, 0, transform.position.z + depth);
        //    streetParameters.streetLength = (streetParameters.startPosition - streetParameters.endPosition).magnitude;
        //    GetComponent<RoadBuilder>()?.SpawnRoad(streetParameters, numberOfSplitsOfTheRoad);
        //}
     
    }
}