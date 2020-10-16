using Demo;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;

[RequireComponent(typeof(RandomGenerator))]
public class CityBuilder : Builder
{
    [SerializeField]
    GameObject buildingPrefab;


    [SerializeField,Range(0, 50)]
    float width =10 ;
    [SerializeField, Range(0, 50)]
    float depth =10;


    [SerializeField]
    int BuildingDistance;

    [SerializeField, Range(0, 20)]
    int MinRoadLength = 10;


    //RandomGenerator random;

    private void Awake()
    {
        base.Awake();
        //random = GetComponent<RandomGenerator>();
        Generate();
    }

    public override void  Generate()
    {
        base.Generate();
        //random.ResetRandom();
        BuildCity();
    }

    void BuildCity() {
        RemoveChildren();
        buildRoads();
    }
    void RemoveChildren() {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }


    void buildRoads() {
        Vector3 randPostionStart = new Vector3(random.Next(0, width), 0, random.Next(0, width));
        Vector3 randPostionEnd;
        do
        {
            randPostionEnd = new Vector3(random.Next(0, width), 0, random.Next(0, width));
        } while ((randPostionStart - randPostionEnd).sqrMagnitude < MinRoadLength * MinRoadLength);

        //Debug.Log((randPostionStart - randPostionEnd).magnitude);       

        GetComponent<RoadBuilder>()?.SpawnRoad(randPostionStart, randPostionEnd);
    }

}
