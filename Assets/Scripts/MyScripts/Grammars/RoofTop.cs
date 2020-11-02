using Demo;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Dynamic;
using UnityEngine;

[System.Serializable]
public class prefabsGroup
{
    [SerializeField]
    public GameObject[] groupPrefabs;

    
}


[RequireComponent(typeof(GridInfo))]
public class RoofTop : Shape
{
    bool wasBuilt = false;


    [Tooltip("Set the roof meshes, 0- edge, 1 -outter corner, 2- reverseCorner, 3-1 square roofTop, 4 - lineEdge ")]
    [SerializeField]
    prefabsGroup[] roofMeshes;


    [SerializeField]
    Material[] roofMaterials;


    [SerializeField]
    public GameObject roofPrefab;

    GridInfo grid;
    private void Awake()
    {
        grid = GetComponent<GridInfo>();
    }

    private GameObject ChooseRightPrefab(int value)
    {
        int random = Random.Range(0, 10);
        switch (value) {
            //edge values
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 128 - 1 - 2:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 2 - 4 - 8:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 32 - 16 - 8:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 128 - 64 - 32:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 8 - 16:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 1 - 2:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 2 - 4:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 32 - 16:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 64 - 128:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 128 - 1:
                return roofMeshes[0].groupPrefabs[0];


            //Outer  corners
            case 1 + 2 + 4:
            case 4 + 8 + 16:
            case 64 + 32 + 16:
            case 1 + 128 + 64:
            case 1 + 2 + 4 + 8:
            case 4 + 8 + 16 + 32:
            case 16 + 32 + 64 + 128:
            case 1 + 2 + 64 + 128:
            case 2 + 4 + 8 + 16 + 32:
            case 2 + 4 + 8 + 16:
            case 8 + 16 + 32 + 64:
                return roofMeshes[1].groupPrefabs[0];

            //Innver corners
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 2:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 8:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 32:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 128:
                return roofMeshes[2].groupPrefabs[0];

           


            case 17:
            case 64 + 4:
            case 1:
            case 16:
            case 64:
            case 4:
                Debug.Log("Edge?");
                return roofMeshes[4].groupPrefabs[0];

            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128:
                return null;

            case 0:
                return roofMeshes[3].groupPrefabs[0];
        }

        return roofMeshes[0].groupPrefabs[1];
    }


    private float getRotation(int value) {
        switch (value) {
            //edge rotations
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 128 - 64 - 32:
                return 2;
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 8 - 16 - 32:
                return 1;
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 1 - 2:
                return 3;
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 128 - 1 - 2:
                return 3;
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 128 - 1:
                return 3;
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 128 - 64:
                return 2;

            //corner rotations

            case 1 + 2 + 4:
                return 2;
            case 4 + 8 + 16:
                return -1;
            case 1 + 64 + 128:
                return 1;
            case 4 + 8 + 16 + 32:

                return 3;

            case 2 + 4 + 8 + 16:
                return 3;


            //inner corner rotation
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 128:
                return 3;
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 8:
                return 1;
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 32:
                return 2;



            //line edge:
            case 17:
            case 1:
            case 16:
                return 1;

        }
        return 0;
    }

    int[,] newFloorPlan() {
        int[,] floorPlan = grid.getArray();

        bool needsAFloor = false;


        //Reset Floor plan
        for (int i = 0; i < floorPlan.GetLength(0); i++) {
            for (int j = 0; j < floorPlan.GetLength(1); j++)
            {
                if (grid.getValue(i, j) != 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128)
                {
                    
                    floorPlan[i, j] = -1;
                }
                else {
                    needsAFloor = true;
                    floorPlan[i, j] = 0;
                }
            }
        }

        Vector2Int startPosition = getStartPositionCopy(floorPlan);
        Debug.LogWarning(startPosition);
        
        if (!needsAFloor) {
            return null;
        }

        //Get a smaller floor Plan
        int[,] copy = new int[floorPlan.GetLength(0) - startPosition.x, floorPlan.GetLength(1)- startPosition.y];

        //Debug.Log(floorPlan.GetLength(0) + ":" + floorPlan.GetLength(1));

        //for (int i = 0; i < copy.GetLength(1); i++) {
        //    for (int j = 0; j < copy.GetLength(0); j++)
        //    {
        //        Debug.Log(i + "==== " + j);
        //        Debug.Log("Start position" + startPosition.x + "==== " + startPosition.y);
        //        copy[i, j] = floorPlan[i + startPosition.x, j+ startPosition.y];
        //    }
        //}


        return floorPlan;
    }


    Vector2Int getStartPositionCopy(int[,] floorPlan) {
        //int[,] floorPlan = floorPla;

        //for (int i = 0; i < floorPlan.GetLength(0); i++)
        //{
        //    for (int j = 0; j < floorPlan.GetLength(1); j++)
        //    {
        //        if (grid.getValue(i, j) != 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128)
        //        {
        //            floorPlan[i, j] = -1;
        //        }
        //        else
        //        {
        //            floorPlan[i, j] = 0;
        //        }
        //    }
        //}
        int iPositionToStartCopy = 0;
        int jPositionToStartCopy = 0;


        for (int i = 0; i < floorPlan.GetLength(0); i++)
        {
            for (int j = 0; j < floorPlan.GetLength(1); j++)
            {
                if (floorPlan[i, j] == 0)
                {
                    jPositionToStartCopy = i;
                    break;
                }
            }

            if (jPositionToStartCopy != -1)
            {
                iPositionToStartCopy = i;
                break;
            }
        }

        return new Vector2Int(iPositionToStartCopy, jPositionToStartCopy);
    }



    public void Initialize(int[,] dataArray)
    {
        grid.Initialize(dataArray.GetLength(1), dataArray.GetLength(0));
        grid.FillTheData(dataArray);
    }


    protected override void Execute()
    {
        if (wasBuilt) return;
        Debug.Log(transform.name);
        GameObject roofElement;
        GameObject roofBlock;
        for (int i = 0; i < grid.Depth; i++)
        {
            for (int j = 0; j < grid.Width; j++)
            {
                roofElement = ChooseRightPrefab(grid.getValue(i, j));

                if (roofElement != null)
                {
                    if (grid.getValue(i, j) < 0) { continue; }
                    roofBlock = SpawnPrefab(roofElement);
                    roofBlock.transform.name = "i[" + i + "]; j[" + j + "]";

                    Debug.Log(grid.getValue(i, j)+ "==" + "i[" + i + "]; j[" + j + "]");

                    roofBlock.transform.localPosition = new Vector3(j, 0, i);

                    float rotation = 90.0f * getRotation(grid.getValue(i, j));
                    roofBlock.transform.localRotation = Quaternion.Euler(0, rotation, 0);
                }
                wasBuilt = true; //Bug with double building temporary fix
            }
        }

        //Should have Extra floors?
        int[,] floorPlan = newFloorPlan();



        if (floorPlan != null)
        {
            Debug.Log("Needs next Level of Roof");
            GameObject roofTop = SpawnPrefab(roofPrefab);
            roofTop.transform.localPosition = new Vector3(0, 1, 0);
            roofTop.transform.name = "RoofTop";

            roofTop.GetComponent<RoofTop>().roofPrefab = roofPrefab;
            roofTop.GetComponent<RoofTop>()?.Initialize(floorPlan);
            roofTop.GetComponent<RoofTop>()?.Generate(0.1f);

        }
        else {
            Debug.Log("Does not need next Level of Roof");
        }


    }
}
