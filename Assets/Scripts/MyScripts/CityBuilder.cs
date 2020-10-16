using Demo;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;



[RequireComponent(typeof(RandomGenerator),typeof(RoadBuilder))]
public class CityBuilder : Builder
{
    [SerializeField]
    GameObject buildingPrefab;


    [SerializeField,Range(5, 250)]
    float width =10 ;
    [SerializeField, Range(5, 250)]
    float depth =10;

    [SerializeField]
    FloatRandomRange distanceBetweenBuildings;

    [SerializeField, Range(0, 50)]
    float MinRoadLength = 10;

    [SerializeField]
    StreetParameters streetParameters;

    private void OnValidate()
    {
        float maxLength = Mathf.Sqrt(width * width + depth * depth);

        if (MinRoadLength >= maxLength)
        {
            MinRoadLength = maxLength * 0.9f;
        }

        distanceBetweenBuildings.Validate();
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
        for (int i = 0; i < 1; i++)
        {
            buildRoad();
        }
        //buildRoad();
    }
    void RemoveChildren() {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void buildRoad() {
        Vector3 randPostionStart = new Vector3(random.Next(0, width), 0, random.Next(0, width));
        Vector3 randPostionEnd;
        do
        {
            randPostionEnd = new Vector3(random.Next(0, width), 0, random.Next(0, width));
        } while ((randPostionStart - randPostionEnd).sqrMagnitude < MinRoadLength * MinRoadLength);


        streetParameters.startPosition = randPostionStart;
        streetParameters.endPosition = randPostionEnd;
        streetParameters.streetLength = (randPostionStart - randPostionEnd).magnitude;

        GetComponent<RoadBuilder>()?.SpawnRoad(streetParameters);
    }
}