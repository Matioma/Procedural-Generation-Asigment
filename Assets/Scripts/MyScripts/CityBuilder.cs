using Demo;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;

public class CityBuilder : MonoBehaviour
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

    private void Awake()
    {
        Generate();
    }

    public void Generate()
    {
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
        Vector3 randPostionStart = new Vector3(Random.Range(0, width), 0, Random.Range(0, depth));
        Vector3 randPostionEnd;
        do
        {
            randPostionEnd = new Vector3(Random.Range(0, width), 0, Random.Range(0, depth));
        } while ((randPostionStart - randPostionEnd).sqrMagnitude < MinRoadLength * MinRoadLength);

        Debug.Log((randPostionStart - randPostionEnd).magnitude);       

        GetComponent<RoadBuilder>()?.SpawnRoad(randPostionStart, randPostionEnd);
    }

}
