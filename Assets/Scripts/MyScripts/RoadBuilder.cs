using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using UnityEngine;

public class RoadBuilder : MonoBehaviour
{
    [SerializeField]
    GameObject RoadPrefab;

  

    public void SpawnRoad(StreetParameters streetParameters)
    {
        var road = Instantiate(RoadPrefab, transform);

        //road.GetComponent<RoadSystemBuilder>().Initialize(2, streetParameters);
        //road.GetComponent<Builder>().Generate();
        road.GetComponent<Road>()?.Initialize(streetParameters);
        road.GetComponent<Road>()?.Generate();
    }

    public void SpawnRoad(StreetParameters streetParameters, int splitsLeft =0)
    {
        var road = Instantiate(RoadPrefab, transform);

        //road.GetComponent<RoadSystemBuilder>().Initialize(2, streetParameters);
        //road.GetComponent<Builder>().Generate();
        road.GetComponent<Road>()?.Initialize(streetParameters, splitsLeft);
        road.GetComponent<Road>()?.Generate();
    }
}
