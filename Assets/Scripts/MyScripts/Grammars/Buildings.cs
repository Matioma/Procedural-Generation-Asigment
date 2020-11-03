using Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : Shape
{
    public GameObject floorPrefab;
    public GameObject RoofPrefab;


    int heightRemaining;
    int width;
    int depth;

 

    [SerializeField]
    public BuildingParameters buildingParameters;


    public int[,] floorPlan;



    public void resetFloorPlan() {
        floorPlan = new int[0, 0];
    }


    //Reset The floor Plan
    public void OnValidate()
    {
        resetFloorPlan();
    }


    /// <summary>
    /// Trigger Building
    /// </summary>
    public void Trigger()
    {
        var random = GetComponent<RandomGenerator>(); 
        
        if (random != null)
        {
            random.ResetRandom();
        }
        if (Root != null)
        {
            UpdateRandomValues();
            Generate();
        }
    }


    private void UpdateRandomValues()
    {
        width = RandomInt(buildingParameters.minWidth, buildingParameters.maxWidth+ 1);
        depth = RandomInt(buildingParameters.minDepth, buildingParameters.maxDepth+1);
        heightRemaining = RandomInt(buildingParameters.minHeight - 1, buildingParameters.maxHeight);
        resetFloorPlan();
    }

    //public void Initialize(int heightRemaining, GameObject prefab, int[,] floorPlan)
    //{
    //    this.heightRemaining = heightRemaining;
    //    this.floorPlan = floorPlan;
    //    floorPrefab = prefab;

    //    width = floorPlan.GetLength(0);
    //    depth = floorPlan.GetLength(1);
    //}


    public void Initialize(int heightRemaining, GameObject prefab, BuildingParameters buildingParameters, int[,] floorPlan)
    {
        
        this.heightRemaining = heightRemaining;
        this.floorPlan = floorPlan;
        floorPrefab = prefab;

        this.buildingParameters = new BuildingParameters(buildingParameters);
        width = floorPlan.GetLength(0);
        depth = floorPlan.GetLength(1);
    }

    private void GenerateFloorPlan( int width,int depth)
    {
        floorPlan = new int[width, depth];
        for (int i = 0; i < width; i++) {
            for (int j = 0; j< depth; j++)
            {
                floorPlan[i, j] = 1;
            }
        }
        randomSquareInCorner();
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


        int depthOfTheBuilding = floorPlan.GetLength(1)-1;
        int widthOfTheBuilding = floorPlan.GetLength(0)-1;

        int corner = RandomInt(0, 2);
        
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
        
        size.x = RandomInt(0, floorPlan.GetLength(0)-1); //Size Along x Axis
        size.y = RandomInt(0, floorPlan.GetLength(1) - 1); //Size Along z Axis

        if (corner == 0)
        {
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
            GenerateFloorPlan(width, depth);
        }

        //Create a floor with a specific plan
        GameObject floorObject = SpawnPrefab(floorPrefab);
        floorObject.GetComponent<Floor>().Initialize(floorPlan); //Set the floor plan

        //Create 2nd floor building
        if (heightRemaining > 0)
        {
            Buildings building = CreateSymbol<Buildings>("BuildingSymbol", new Vector3(0, 1, 0));
            building.RoofPrefab = RoofPrefab;

            building.Initialize(heightRemaining - 1, floorPrefab, buildingParameters, floorPlan);
            building.Generate(0.1f);
        }
        //Create Roof
        else {
            GameObject roofTop = SpawnPrefab(RoofPrefab);
            roofTop.transform.localPosition = new Vector3(0, 1, 0);
            roofTop.transform.name = "RoofTop";

            roofTop.GetComponent<RoofTop>().roofPrefab = RoofPrefab;
           
            int[,] array = new int[depth, width];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < depth; j++)
                {
                    array[j, i] = floorPlan[i, j] - 1; // (Roof top data -1 is and empty value)
                }
            }
            roofTop.GetComponent<RoofTop>()?.Initialize(array);
            roofTop.GetComponent<RoofTop>()?.Generate(0.1f);
        }
    }
}
