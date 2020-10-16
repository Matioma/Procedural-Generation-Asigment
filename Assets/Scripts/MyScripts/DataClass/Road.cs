
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
        //Debug.Log(curve.keys[0].time);

        streetParameters.buildingDensity.Validate();
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
        for (distancePassed = 0; distancePassed < distance; distancePassed += buildingDinstance) {

            buildingDinstance = random.Next(streetParameters.buildingDensity.minValue, streetParameters.buildingDensity.maxValue);

            Vector3 objectPosition = streetParameters.startPosition + vectorDif.normalized * distancePassed; //+ perpendicular * curve.Evaluate(distancePassed);

            //Buildings of Left
            GameObject building = Instantiate(buildingPrefab, child.transform);
            building.GetComponent<RandomGenerator>().seed = random.Next(int.MaxValue); //Random.Range(0, int.MaxValue);
            building.transform.position = objectPosition + perpendicular * streetParameters.roadWidth / 2;

            //Building on Right
            building = Instantiate(buildingPrefab, child.transform);
            building.GetComponent<RandomGenerator>().seed = random.Next(int.MaxValue);// Random.Range(0, int.MaxValue);
            building.transform.position = objectPosition - perpendicular * streetParameters.roadWidth / 2;

            
        }

        //Debug.Log(distance + " -  " + distance / 2);
        if (distance / 2 > minLength) {
            SplitRoad(distancePassed / 2, true);
        }
    }


    public void Initialize(StreetParameters streetParameters)
    {
        this.streetParameters = new StreetParameters(streetParameters);
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


    void SplitRoad(float time, bool left) {
        StreetParameters newStreetParameters = new StreetParameters(streetParameters);
        newStreetParameters.streetLength *= 0.5f;
        newStreetParameters.startPosition = streetParameters.startPosition + vectorDif.normalized * time;

        

        float degreesFromRoadNormal = random.Next(-45,45);
        Debug.Log("Road was split");


        Vector3 direction = Quaternion.Euler(0, degreesFromRoadNormal, 0) * perpendicular;
        newStreetParameters.endPosition = newStreetParameters.startPosition + direction * newStreetParameters.streetLength;



        var newRoad = Instantiate(roadPrefab,transform);
        newRoad.GetComponent<Road>()?.Initialize(newStreetParameters);
        newRoad.GetComponent<Road>()?.Generate();
        //newRoad.GetComponent<Road>().streetParameters = newStreetParameters;


        //Debug.DrawLine(streetParameters.startPosition, streetParameters.endPosition, Color.cyan, 5.0f);
        //Debug.DrawLine(newStreetParameters.startPosition, newStreetParameters.endPosition, Color.green,5.0f);
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

