using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(RandomGenerator))]
public class Road:Builder
{
    [SerializeField]
    GameObject buildingPrefab;
    [SerializeField]
    public Vector3 startPosition;
    [SerializeField]
    public Vector3 endPosition;
    
    [SerializeField]
    public AnimationCurve curve;

    [SerializeField, Range(1,15)]
    float buildingDinstance=2;

    [SerializeField, Range(0, 150)]
    float curvitureDepth = 20;


    [SerializeField,Range(0,20)]
    float roadWidth = 5;


    [SerializeField]
    FloatRandomRange buildingDensity;


    public Vector3 vectorDif {
        get { return endPosition - startPosition; }
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
        buildingDensity.Validate();
    }

    override public void Generate() {
        base.Generate();
        RemoveChildren();
        RandomizeCurve();
        BuildTheBuildings();
    }


    void BuildTheBuildings() {
        float distance = vectorDif.magnitude;

        buildingDinstance = random.Next(buildingDensity.minValue, buildingDensity.maxValue);
       

        float distancePassed;
        for (distancePassed = 0; distancePassed < distance; distancePassed += buildingDinstance) {

            buildingDinstance = random.Next(buildingDensity.minValue, buildingDensity.maxValue);

            Vector3 objectPosition = startPosition + vectorDif.normalized * distancePassed + perpendicular * curve.Evaluate(distancePassed);

            //Buildings of Left
            GameObject building = Instantiate(buildingPrefab, transform);
            building.GetComponent<RandomGenerator>().seed = random.Next(int.MaxValue); //Random.Range(0, int.MaxValue);
            building.transform.position = objectPosition + perpendicular * roadWidth / 2;

            //Building on Right
            building = Instantiate(buildingPrefab, transform);
            building.GetComponent<RandomGenerator>().seed = random.Next(int.MaxValue);// Random.Range(0, int.MaxValue);
            building.transform.position = objectPosition - perpendicular * roadWidth / 2;
        }
    }



    public void  Initialize(Vector3 start, Vector3 end, FloatRandomRange range) {
        startPosition = start;
        endPosition = end;
        buildingDensity = range;

        //Debug.Log(buildingDensity.minValue + ": " + buildingDensity.maxValue);
        buildingDinstance = random.Next(buildingDensity.minValue, buildingDensity.maxValue);
        RandomizeCurve();
    }

    public void RandomizeCurve() {
        //Debug.Log(curvitureDepth);
     

        for (int i = 0; i < curve.length; i++) {
            curve.RemoveKey(i);
        }
        curve.AddKey(0, 0);
        curve.MoveKey(0, new Keyframe(0, 0, 0, 0));

        //Debug.Log(vectorDif.magnitude);
        curve.MoveKey(1, new Keyframe(vectorDif.magnitude, 0,0,0));

        float randomX = random.Next(curve[0].time, curve[curve.length - 1].time);
        float randomY = random.Next(-curvitureDepth, curvitureDepth) -0.5f;


        Keyframe keyFrame = new Keyframe(randomX, randomY,0,0);

        curve.AddKey(keyFrame);
        //curve.AddKey(randomX, randomY);
        //=1.0f;
        //curve.keys[1].inTangent = 1.0f;

        //Debug.Log(curve.keys[1].inTangent + ":" + curve.keys[1].outTangent);
    }

    public void RemoveChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}

