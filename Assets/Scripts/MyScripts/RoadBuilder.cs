using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using UnityEngine;

public class RoadBuilder : MonoBehaviour
{
    [SerializeField]
    GameObject RoadPrefab;

    public void SpawnRoad(Vector3 start, Vector3 end) {
        var road = Instantiate(RoadPrefab, transform);
        road.transform.position = start;
        road.GetComponent<Road>()?.Initialize(start, end);
        road.GetComponent<Road>()?.Generate();
    }
}
