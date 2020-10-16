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

        road.GetComponent<Road>()?.Initialize(streetParameters);
        road.GetComponent<Road>()?.Generate();
    }
}
