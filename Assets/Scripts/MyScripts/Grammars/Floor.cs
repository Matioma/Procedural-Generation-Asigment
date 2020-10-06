using Demo;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Floor : Shape
{
    public GameObject wallPrefab;

    public int width;
    public int depth;


    public int rotationsLeft = 3;



    public int[,] floorPlan;


  


    public void Initialize(int pRotationsLeft, GameObject prefab)
    {
        rotationsLeft = pRotationsLeft;
        wallPrefab = prefab;
    }

    public void Initialize(int [,] floorPlan)
    {
        this.floorPlan = floorPlan;


        this.width = floorPlan.GetLength(0);
        this.depth = floorPlan.GetLength(1);
    }

    protected override void Execute()
    {
        GameObject wall = SpawnPrefab(wallPrefab);
        wall.transform.localPosition = new Vector3(0, 0, 0);
        wall.GetComponent<Wall>().WidthRemaining = depth;

        wall = SpawnPrefab(wallPrefab);
        wall.transform.localPosition = new Vector3(0, 0, depth-1);
        wall.transform.localRotation = Quaternion.Euler(0, 90, 0);
        wall.GetComponent<Wall>().WidthRemaining = width;

        wall = SpawnPrefab(wallPrefab);
        wall.transform.localPosition = new Vector3(width-1, 0, depth - 1);
        wall.transform.localRotation = Quaternion.Euler(0, 180, 0);
        wall.GetComponent<Wall>().WidthRemaining = depth;


        wall = SpawnPrefab(wallPrefab);
        wall.transform.localPosition = new Vector3(width - 1, 0, 0);
        wall.transform.localRotation = Quaternion.Euler(0, 270, 0);
        wall.GetComponent<Wall>().WidthRemaining = width;

       
    }
}
