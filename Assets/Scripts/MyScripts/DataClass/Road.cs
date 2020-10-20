
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

    AnimationCurve oldCurve;

    [Header ("Road Options")]
    [SerializeField]
    bool roadToLeft = true;
    [SerializeField]
    bool roadToRight = true;

    [Header("Road Options")]
    [SerializeField]
    bool buildingsToRight = true;
    [SerializeField]
    bool buildingsToLeft = true;

    [SerializeField]
    int splitsLeft = 0; 

    public Vector3 vectorDif {
        get {
            //Debug.Log(streetParameters.endPosition + ": " + streetParameters.startPosition);
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
        

        float distancePassed;
        for (distancePassed = streetParameters.intersectionsSize; distancePassed < distance - streetParameters.intersectionsSize; distancePassed += buildingDinstance) {

            buildingDinstance = random.Next(streetParameters.buildingDensity.minValue, streetParameters.buildingDensity.maxValue);

            Vector3 objectPosition = streetParameters.startPosition + vectorDif.normalized * distancePassed+ perpendicular * curve.Evaluate(distancePassed);


            if (buildingsToLeft) {
                GameObject building = Instantiate(buildingPrefab, child.transform);
                building.GetComponent<RandomGenerator>().seed = random.Next(int.MaxValue); 
                building.transform.position = objectPosition + perpendicular * streetParameters.roadWidth / 2;

               // Debug.Log()s
            }
            //Building on Right
            if (buildingsToRight) {
                GameObject building = Instantiate(buildingPrefab, child.transform);
                building.GetComponent<RandomGenerator>().seed = random.Next(int.MaxValue);
                building.transform.position = objectPosition - perpendicular * streetParameters.roadWidth / 2;
            }
        }

        if(splitsLeft>0)
        {
            if (roadToLeft)
            {
                SplitRoad(distancePassed / 2);
            }
            if(roadToRight){
                SplitRoad(distancePassed / 2, left: false);
            }
        }

       
    }


    public void Initialize(StreetParameters streetParameters, int splitsLeft=0)
    {
        this.streetParameters = new StreetParameters(streetParameters);
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

        float randomX = random.Next(curve[0].time, curve[curve.length - 1].time);
        float randomY = random.Next(-streetParameters.curvitureDepth, streetParameters.curvitureDepth);


        Keyframe keyFrame = new Keyframe(randomX, randomY, 0, 0);

       

        curve.AddKey(keyFrame);
       
    }


    void SplitRoad(float time, bool left =true) {
        StreetParameters newStreetParameters = new StreetParameters(streetParameters);


        newStreetParameters.streetLength = newStreetParameters.GetStreetLength()/2;
        newStreetParameters.startPosition = streetParameters.startPosition + vectorDif.normalized * time;

        

        float degreesFromRoadNormal = random.Next(-45,45);
        Debug.Log("Road was split");

        Vector3 roadDirection = new Vector3(perpendicular.x, perpendicular.y, perpendicular.z);
        if (!left) {
            roadDirection *= -1;
        }


        Vector3 direction = Quaternion.Euler(0, degreesFromRoadNormal, 0) * roadDirection;
        newStreetParameters.endPosition = newStreetParameters.startPosition + direction * newStreetParameters.streetLength;


        GetComponent<RoadBuilder>().SpawnRoad(newStreetParameters,splitsLeft-1);

        //var newRoad = Instantiate(roadPrefab,transform);
        //newRoad.GetComponent<Road>()?.Initialize(newStreetParameters);
        //newRoad.GetComponent<Road>()?.Generate();
        //newRoad.GetComponent<Road>().streetParameters = newStreetParameters;


        //Debug.DrawLine(streetParameters.startPosition, streetParameters.endPosition, Color.cyan, 5.0f);
        //Debug.DrawLine(newStreetParameters.startPosition, newStreetParameters.endPosition, Color.green, 5.0f);
        //Debug.DrawRay(newStreetParameters.startPosition, perpendicular * 100, Color.yellow, 5.0f);
    }

    public void RemoveChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}

