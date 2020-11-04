using Demo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    public GameObject roofPrefab;

    [SerializeField, Header("Material Settings")]
    Material roofMaterials;

    GridInfo grid;

    RandomGenerator random;


    [SerializeField, Header("Patterns")]
    Pattern EdgesPattern;
    [SerializeField]
    Pattern OuterCornerPattern;
    [SerializeField]
    Pattern ReverseCornerPattern;
    [SerializeField]
    Pattern SquareRoofPattern;
    [SerializeField]
    Pattern lineEdgePattern;


    private void Awake()
    {
        grid = GetComponent<GridInfo>();
        random = GetComponent<RandomGenerator>();
    }


    private GameObject ChooseRightPrefab(int value)
    {
        int index=0;

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
                if (EdgesPattern.Length > 0)
                {
                    index = EdgesPattern.GetValue();
                }
                else {
                    index = random.Next(0, roofMeshes[0].groupPrefabs.Length);
                }

                
                return roofMeshes[0].groupPrefabs[index];
                break;

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
                if (OuterCornerPattern.Length > 0)
                {
                    index = OuterCornerPattern.GetValue();
                }else {
                    index = random.Next(0, roofMeshes[1].groupPrefabs.Length);
                }
                return roofMeshes[1].groupPrefabs[index];
                break;

            //Innver corners
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 2:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 8:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 32:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 128:
                if (ReverseCornerPattern.Length > 0)
                {
                    index = EdgesPattern.GetValue();
                }
                else {
                    index = random.Next(0, roofMeshes[2].groupPrefabs.Length);
                }
                
                return roofMeshes[2].groupPrefabs[0];
                break;


            //Solo square
            case 0:
                if (SquareRoofPattern.Length > 0)
                {
                    index = SquareRoofPattern.GetValue();
                }
                else {
                    index = random.Next(0, roofMeshes[3].groupPrefabs.Length);
                }

                
                return roofMeshes[3].groupPrefabs[index];



            //Line Edge
            case 17:
            case 64 + 4:
            case 1:
            case 16:
            case 64:
            case 4:
                if (lineEdgePattern.Length > 0)
                {
                    index = lineEdgePattern.GetValue();
                }
                else {
                    index = random.Next(0, roofMeshes[4].groupPrefabs.Length);

                }
                return roofMeshes[4].groupPrefabs[index];

            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128:
                return null;
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


        
        int[,] floorPlan = new int[grid.getArray().GetLength(0), grid.getArray().GetLength(1)];
        Array.Copy(grid.getArray(), floorPlan, grid.getArray().Length);
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
        
        if (!needsAFloor) {
            return null;
        }


        return floorPlan;
    }
    public void Initialize(int[,] dataArray)
    {
        grid.Initialize(dataArray.GetLength(1), dataArray.GetLength(0));
        grid.FillTheData(dataArray);
    }


    public void Generate() {
        //Debug.Log("Generated");
        DeleteGenerated();
        if (random != null)
        {
            random.ResetRandom();
        }
        wasBuilt = false;
        Execute();
    }

    protected override void Execute()
    {
        if (wasBuilt) return;

        //Debug.Log(transform.name);
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

                    MeshRenderer meshRenderer = roofBlock.GetComponentInChildren<MeshRenderer>();
                    var materialList = meshRenderer.materials;



                    materialList[0] = roofMaterials;
                    if (materialList.Length > 1) {
                        materialList[1] = roofMaterials;
                    }
                    if (materialList.Length > 2)
                    {
                        materialList[2] = roofMaterials;
                    }
                    if (materialList.Length > 3)
                    {
                        materialList[3] = roofMaterials;
                    }
                   
                    meshRenderer.materials = materialList;

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
        else
        {
            //Debug.Log("Does not need next Level of Roof");
        }


    }
}
