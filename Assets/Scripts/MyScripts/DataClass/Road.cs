
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(RandomGenerator))]
public class Road : Builder
{
    [SerializeField]
    GameObject buildingPrefab;
    [SerializeField]
    GameObject roadPrefab;

    [SerializeField]
    public AnimationCurve curve;
    [SerializeField]
    public StreetParameters streetParameters;

    [SerializeField]
    float minLength = 10;


    [SerializeField]
    BuildingParameters buildingParameters;

    AnimationCurve oldCurve;

    [Header("Road Options")]
    [SerializeField]
    bool roadToLeft = true;
    [SerializeField]
    bool roadToRight = true;

    [Header("Buildings Options")]
    [SerializeField]
    bool buildingsToRight = true;
    [SerializeField]
    bool buildingsToLeft = true;

    [SerializeField]
    int splitsLeft = 0;


    float timeSplitLeft;
    float timeSplitRight;



    List<GameObject> PredifinedBuldings;


    public bool SetHasBuildingsOnLeft{
        set {
            buildingsToLeft = value;
        }
    }
    public bool SetHasBuildingsOnRight
    {
        set
        {
            buildingsToRight = value;
        }
    }





    public Vector3 vectorDif {
        get {
            return streetParameters.endPosition - streetParameters.startPosition;
        }
    }

    public Vector3 perpendicular
    {
        get { return Vector3.Cross(vectorDif, Vector3.up).normalized; }
    }

    override protected void Awake()
    {
        base.Awake();
    }

    private void OnValidate()
    {
        streetParameters.buildingDensity.Validate();

        if (!buildingsToRight) {
            roadToRight = false;
        }
        if (!buildingsToLeft) {
            roadToLeft = false;
        }
    }

    override public void Generate() {
        base.Generate();
        RemoveChildren();


        GenerateTheRoadCurve();
        PlaceBuildingsAlongTheRoad();
    }

    public void Generate(bool withNewCurve = true)
    {
        base.Generate();
        RemoveChildren();
        if (withNewCurve) {
            GenerateTheRoadCurve();
        }
        PlaceBuildingsAlongTheRoad();
    }

    void PlaceBuildingsAlongTheRoad() {
        float distance = vectorDif.magnitude;


        float buildingDinstance = random.Next(streetParameters.buildingDensity.minValue, streetParameters.buildingDensity.maxValue);


        GameObject child = new GameObject();
        child.transform.parent = this.transform;
        child.transform.name = "Road buildings";


        //Compute the split positions
        if (splitsLeft > 0)
        {
            if (roadToLeft)
            {
                timeSplitLeft = random.Next(0, distance);
            }
            if (roadToRight)
            {
                timeSplitRight = random.Next(0, distance);
            }
        }


        float distancePassed;


        //Spawn Building along the road
        for (distancePassed = streetParameters.intersectionsSize; distancePassed < distance - streetParameters.intersectionsSize; distancePassed += buildingDinstance) {

            buildingDinstance = random.Next(streetParameters.buildingDensity.minValue, streetParameters.buildingDensity.maxValue);

            Vector3 objectPosition = streetParameters.startPosition + vectorDif.normalized * distancePassed+ perpendicular * curve.Evaluate(distancePassed);

            if (buildingsToLeft) {
                if (!roadToLeft || Mathf.Abs(timeSplitLeft - distancePassed) >= streetParameters.intersectionsSize)
                {
                   
                        GameObject building = Instantiate(buildingPrefab, child.transform);

                        building.GetComponent<RandomGenerator>().seed = random.Next(int.MaxValue);
                        building.transform.position = objectPosition + perpendicular * streetParameters.roadWidth / 2;


                        Vector3 nextPosition = streetParameters.startPosition + vectorDif.normalized * distancePassed + perpendicular * curve.Evaluate(distancePassed + 0.001f);
                        Vector3 relativePosition = nextPosition - building.transform.position;
                        Vector3 directionVector = Vector3.Cross(relativePosition, Vector3.up).normalized;
                        building.transform.rotation = Quaternion.LookRotation(-directionVector, Vector3.up);

                        building.GetComponent<Buildings>().buildingParameters = new BuildingParameters(this.buildingParameters);
                        building.GetComponent<Buildings>().Trigger();
                   

                  
                }
            }
            //Building on Right
            if (buildingsToRight) {
                if ( !roadToRight || Mathf.Abs(timeSplitLeft - distancePassed) >= streetParameters.intersectionsSize )
                {
                   
                        GameObject building = Instantiate(buildingPrefab, child.transform);
                        building.GetComponent<RandomGenerator>().seed = random.Next(int.MaxValue);
                        building.transform.position = objectPosition - perpendicular * streetParameters.roadWidth / 2;


                        Vector3 nextPosition = streetParameters.startPosition + vectorDif.normalized * distancePassed + perpendicular * curve.Evaluate(distancePassed + 0.001f);
                        Vector3 relativePosition = nextPosition - building.transform.position;
                        Vector3 directionVector = Vector3.Cross(relativePosition, Vector3.up).normalized;
                        building.transform.rotation = Quaternion.LookRotation(directionVector, Vector3.up);

                        building.GetComponent<Buildings>().buildingParameters = new BuildingParameters(this.buildingParameters);
                        building.GetComponent<Buildings>().Trigger();
                  
                   
                }
            }
        }

        if(splitsLeft>0)
        {
            if (roadToLeft)
            {
              
                SplitRoad(timeSplitLeft);
            }
            if(roadToRight){
               
                SplitRoad(timeSplitRight, left: false);
            }
        }
    }

    public void Initialize(StreetParameters streetParameters, BuildingParameters buildingParameters, int splitsLeft = 0)
    {
        
        this.streetParameters = new StreetParameters(streetParameters);
        this.buildingParameters = new BuildingParameters(buildingParameters);
        this.splitsLeft = splitsLeft;
        roadToLeft = true;
        roadToRight = true;
        buildingsToLeft = true;
        buildingsToRight = true;

        GenerateTheRoadCurve();
    }

    void GenerateTheRoadCurve() {
        for (int i = 0; i < curve.length; i++)
        {
            curve.RemoveKey(i);
        }
        curve.AddKey(0, 0);
        curve.MoveKey(0, new Keyframe(0, 0, 0, 0));
        curve.MoveKey(1, new Keyframe(vectorDif.magnitude, 0, 0, 0));

        random = GetComponent<RandomGenerator>();

        float randomX = random.Next(curve[0].time, curve[curve.length - 1].time);
        float randomY = random.Next(-streetParameters.curvitureDepth, streetParameters.curvitureDepth);


        Keyframe keyFrame = new Keyframe(randomX, randomY, 0, 0);

       

        curve.AddKey(keyFrame);
       
    }


    void SplitRoad(float time, bool left =true) {
        StreetParameters newStreetParameters = new StreetParameters(streetParameters);

        newStreetParameters.streetLength = newStreetParameters.GetStreetLength()*0.3f;
        newStreetParameters.startPosition = streetParameters.startPosition + vectorDif.normalized * time;

        float degreesFromRoadNormal = random.Next(-45,45);

        Vector3 roadDirection = new Vector3(perpendicular.x, perpendicular.y, perpendicular.z);
        if (!left) {
            roadDirection *= -1;
        }


        Vector3 direction = Quaternion.Euler(0, degreesFromRoadNormal, 0) * roadDirection;
        newStreetParameters.endPosition = newStreetParameters.startPosition + direction * newStreetParameters.streetLength;
        newStreetParameters.roadWidth *=0.8f;

        GetComponent<RoadBuilder>().SpawnRoad(newStreetParameters, buildingParameters,splitsLeft-1);
    }


    public void RemoveChildren()
    {
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}

