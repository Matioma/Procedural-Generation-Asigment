using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Road:MonoBehaviour
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

    [SerializeField, Range(0, 40)]
    float curvitureDepth = 20;


    [SerializeField,Range(0,20)]
    float roadWidth = 5;

    public Vector3 vectorDif {
        get { return endPosition - startPosition; }
    }

    public Vector3 perpendicular
    {
        get { return Vector3.Cross(vectorDif, Vector3.up).normalized; }
    }

    public void Generate() {
        RemoveChildren();
        RandomizeCurve();
        BuildTheBuildings();
    }

    void BuildTheBuildings() {
        float distance = vectorDif.magnitude;
        float numberOfBuildings = distance / buildingDinstance;
        float distancePerStep = distance /numberOfBuildings;

        for (int i = 0; i < numberOfBuildings; i++)
        {
            Vector3 objectPosition = startPosition + vectorDif.normalized * i * distancePerStep + perpendicular * curve.Evaluate(distancePerStep * i);

            //Buildings of Left
            GameObject building = Instantiate(buildingPrefab, transform);
            building.GetComponent<RandomGenerator>().seed = Random.Range(0, int.MaxValue);
            building.transform.position = objectPosition + perpendicular* roadWidth/2;  

            //Building on Right
            building = Instantiate(buildingPrefab, transform);
            building.GetComponent<RandomGenerator>().seed = Random.Range(0, int.MaxValue);
            building.transform.position = objectPosition - perpendicular * roadWidth / 2;
        }
    }





    public void  Initialize(Vector3 start, Vector3 end) {
        startPosition = start;
        endPosition = end;
        RandomizeCurve();
    }

    public void RandomizeCurve() {
        Debug.Log(curvitureDepth);
     

        for (int i = 0; i < curve.length; i++) {
            curve.RemoveKey(i);
        }
        curve.AddKey(0, 0);
        
        curve.MoveKey(1, new Keyframe(vectorDif.magnitude, 0));

        float randomX = Random.Range(curve[0].time, curve[curve.length - 1].time);
        float randomY = Random.Range(-curvitureDepth, curvitureDepth);

    
        curve.AddKey(randomX, randomY);
        Debug.Log(curve.length);

    }

    public void RemoveChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }


    //void OnDrawGizmos()
    //{
    //    // Draw a yellow sphere at the transform's position
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(startPosition, 0.5f);
    //    Gizmos.DrawSphere(endPosition, 0.5f);
    //}


}

