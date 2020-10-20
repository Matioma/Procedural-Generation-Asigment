using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSystemBuilder : Builder
{
    [SerializeField]
    GameObject RoadPrefab;



    StreetParameters streetParameters;
    int splitsLeft;

    override protected void Awake()
    {
        base.Awake();
    }


    public void Initialize(int splitsLeft, StreetParameters streetParameters)
    {
        this.splitsLeft = splitsLeft;
        this.streetParameters = new StreetParameters(streetParameters);
    }

    public override void Generate()
    {
        base.Generate();
        var road = Instantiate(RoadPrefab, transform);
        road.transform.name = "Road System Builder";

        road.GetComponent<Road>().Initialize(streetParameters);
        road.GetComponent<Road>().Generate();
        //streetParameters

    }

    
}
