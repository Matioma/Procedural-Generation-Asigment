using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;

public class CityBuilder : MonoBehaviour
{
    [SerializeField]
    GameObject buildingPrefab;


    [SerializeField]
    int BuildingDistance;

    private void Awake()
    {
        Generate();
    }

    public void Generate()
    {
        BuildCity();
    }

    void BuildCity() {
        RemoveChildren();
        for (int i = 0; i < 10; i++) {
            var obj =Instantiate(buildingPrefab, transform);
            obj.transform.localPosition = new Vector3(i * BuildingDistance, 0, i * BuildingDistance);
        }
    }

    void RemoveChildren() {
        for (int i = 0; i < transform.childCount; i++) {
            //DestroyObject();
            //transform.GetChild(i).De
            Destroy(transform.GetChild(i).gameObject);
        }
        
    
    }

}
