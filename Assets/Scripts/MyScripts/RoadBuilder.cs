using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using UnityEngine;

public class RoadBuilder : MonoBehaviour
{
    [SerializeField]
    GameObject RoadPrefab;

  
    public void SpawnRoad(StreetParameters streetParameters, BuildingParameters buildingParameters, int splitsLeft = 0)
    {
        var road = Instantiate(RoadPrefab, transform);
        road.GetComponent<Road>()?.Initialize(streetParameters, buildingParameters, splitsLeft);
        road.GetComponent<Road>()?.Generate();
    }
}
