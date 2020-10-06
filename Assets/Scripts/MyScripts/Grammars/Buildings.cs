using Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : Shape
{
    public GameObject floorPrefab;
    public GameObject RoofPrefab;
    public GameObject debugPrefab;

    public int heightRemaining;


    public int width;
    public int depth;



    public int[,] floorPlan;

    void Initialize(int heightRemaining, GameObject prefab) {
        this.heightRemaining = heightRemaining;
        floorPrefab = prefab;
        
    }

    public void resetFloorPlan() {
        floorPlan = new int[0, 0];
    }


    //Reset The floor Plan
    public void OnValidate()
    {
        resetFloorPlan();
    }

    void Initialize(int heightRemaining, GameObject prefab, int[,] floorPlan)
    {
        this.heightRemaining = heightRemaining;
        this.floorPlan = floorPlan;
        floorPrefab = prefab;

        width = floorPlan.GetLength(0);
        depth = floorPlan.GetLength(1);
    }


    private void InitializeFloorPlan( int width,int depth)
    {
        floorPlan = new int[width, depth];
        for (int i = 0; i < width; i++) {
            for (int j = 0; j< depth; j++)
            {
                floorPlan[i, j] = 1;

                //var pref =SpawnPrefab(debugPrefab);
                //pref.transform.localPosition= new Vector3(i, 9, j);
            }
        }
        randomSquareInCorner();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < depth; j++)
            {
                if (floorPlan[i, j] == 1) {
                    var pref = SpawnPrefab(debugPrefab);
                    pref.transform.localPosition = new Vector3(i, 9, j);
                }

             
            }
        }

    }

    private void randomSquareInCorner() {
        // the bulding should be at least 2 square wide to make L shape
        if (floorPlan.GetLength(1) <= 1)
        {
            return;
        }
        //// the bulding should be at least 2 square wide to make L shape
        if (floorPlan.GetLength(0) <= 1)
        {
            return;
        }

        Vector2Int cornerIndex = new Vector2Int(0, 0);
        Vector2Int size = new Vector2Int(0,0);

        // 0 - [0; depth] // topLeft
        // 1 - [width;depth] //topRight
        


        int depthOfTheBuilding = floorPlan.GetLength(1)-1;
        int widthOfTheBuilding = floorPlan.GetLength(0)-1;

        int corner = Random.Range(0, 2);
        
        // Get the corner INdexes

        if (corner == 0)
        {
            cornerIndex.x = 0;
            cornerIndex.y = depthOfTheBuilding;
        }
        else {
            cornerIndex.x = widthOfTheBuilding;
            cornerIndex.y = depthOfTheBuilding;
        }


        
        size.x = Random.Range(1, floorPlan.GetLength(0)-1); //Size Along x Axis
        size.y = Random.Range(1, floorPlan.GetLength(1) - 1); //Size Along z Axis

        if (corner == 0)
        {
            //Debug.Log("Corner Type is : " + corner + ": " + cornerIndex);
            //Debug.Log("Square size " + size.x + ":" + size.y);
            //Debug.Log("Building Sizes are: " + widthOfTheBuilding + ":" + depthOfTheBuilding);

            for (int i = cornerIndex.x; i < size.x; i++)
            {
                for (int j = cornerIndex.y; j > (depthOfTheBuilding - size.y); j--)
                {
                    floorPlan[i, j] = 0;
                }
            }
        }
        else {
            for (int i = cornerIndex.x; i > widthOfTheBuilding-size.x; i--)
            {
                for (int j = cornerIndex.y; j > (depthOfTheBuilding - size.y); j--)
                {
                    floorPlan[i, j] = 0;
                }
            }
        }
    }



    protected override void Execute()
    {
        //If plan of the building was not generted yet
        if(floorPlan ==null || floorPlan.Length == 0)
        {
            InitializeFloorPlan(width, depth);
        }
       
        //Create a floor with a specific plan
        GameObject floorObject = SpawnPrefab(floorPrefab);
        floorObject.GetComponent<Floor>().Initialize(floorPlan); //Set the floor plan


        //Create 2nd floor building
        if (heightRemaining > 0)
        {
            Buildings building = CreateSymbol<Buildings>("Floor", new Vector3(0, 1, 0));
            building.RoofPrefab = RoofPrefab;

            building.Initialize(heightRemaining - 1, floorPrefab, floorPlan);
            building.Generate(0.1f);
        }
        //Create Roof
        else {
            GameObject roofTop = SpawnPrefab(RoofPrefab);
            roofTop.transform.localPosition = new Vector3(0, 1, 0);
            roofTop.transform.name = "RoofTop";

            int[,] array = new int[depth,width];
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < depth; j++) {
                    array[j, i] = floorPlan[i, j]-1;
                }
            }


            //for (int i = 0; i < width; i++) {
            //    array[depth - 1, i] = 0;
            //    array[0, i] = 0;
            //}
            //for (int i = 0; i < depth; i++){
            //    array[i, 0] = 0;
            //    array[i, width - 1] = 0;
            //}

            roofTop.GetComponent<RoofTop>()?.Initialize(array);
            roofTop.GetComponent<RoofTop>()?.Generate(0.1f);
        }
    }
}
