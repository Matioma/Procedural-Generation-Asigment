using Demo;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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


    [Tooltip("Set the roof meshes, 0- edge, 1 - corner, 2- reverseCorner, 3-plane")]
    [SerializeField]
    prefabsGroup[] roofMeshes;


    [SerializeField]
    Material[] roofMaterials;
     

    GridInfo grid;
    private void Awake()
    {
        grid = GetComponent<GridInfo>();
    }

    private GameObject ChooseRightPrefab(int value)
    {
        int random = Random.Range(0,10);
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
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 128 -1:
                return roofMeshes[0].groupPrefabs[0];


            //Outer  corners
            case 1 + 2 + 4:
            case 4 + 8 + 16:
            case 64 + 32 + 16:
            case 1 + 128 + 64:
            case 1+2+4+8:
            case 4+8+16+32:
            case 16 + 32 + 64 +128:
            case 1+2 +64+128:

            case 2 + 4 + 8 + 16+32:
            case 2 + 4 + 8 + 16:
                return roofMeshes[1].groupPrefabs[0];

            //Innver corners
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 -2:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128- 8:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 -32:
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 -128:
                return roofMeshes[2].groupPrefabs[0];


                //case 12:
                //case 6:
                //case 3:
                //case 9:
                //    return roofMeshes[1].groupPrefabs[0];
                //    break;

                //Edge values
                //case 7:
                //case 14:
                //case 13:
                //case 11:
                //    random =RandomInt(0, roofMeshes[0].groupPrefabs.Length);
                //    //random = Random.Range(0, roofMeshes[0].groupPrefabs.Length);
                //    return roofMeshes[0].groupPrefabs[random];
                //    break;
        }

        return roofMeshes[0].groupPrefabs[1];
    }


    private float getRotation(int value) {
        switch (value) {
            //edge rotations
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 128 - 64 - 32:
                return 2;
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 -8-16-32:
                return 1;
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 -1-2:
                return 3;
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 128 - 1 -2:
                return 3;
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 128 - 1:
                return 3;
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 128 - 64:
                return 2;

            //corner rotations

            case 1 + 2 + 4:
                return 2;
            case 4+8+16:
                return -1;
            case 1 + 64 + 128:
                return 1;
            case 4+8+16+32:

                return 3;

            case 2+ 4 + 8 + 16:
                return 3;


            //inner corner rotation
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 128:
                return 3;
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 8:
                return 1;
            case 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 - 32:
                return 2;

                //case 9:
                //    return 1;
                //case 12:
                //    return 2;
                //case 6:
                //    return 3;

                //case 13:
                //    return 1;
                //case 14:
                //    return 2;
                //case 7:
                //    return 3;
        }
        return 0;
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
        for (int i = 0; i < grid.Depth; i++) {
            for (int j = 0; j < grid.Width; j++) {
                roofElement = ChooseRightPrefab(grid.getValue(i,j));

                Debug.Log(grid.getValue(i, j));
                if (grid.getValue(i, j) < 0) { continue; }
               

                roofBlock = SpawnPrefab(roofElement);
                roofBlock.transform.name = "i[" + i + "]; j[" + j + "]";

                Debug.Log(roofBlock.transform.name + ": " +grid.getValue(i, j));

                //Debug.Log(roofBlock.GetComponentInChildren<MeshRenderer>());
                //roofBlock.GetComponentInChildren<MeshRenderer>().material = roofMaterials[0];
                //Debug.Log(roofBlock.GetComponent<MeshRenderer>());

                roofBlock.transform.localPosition= new Vector3(j,0,i);

                //Debug.Log(roofBlock.name + " : " + grid.getValue(i, j));
                float rotation = 90.0f * getRotation(grid.getValue(i, j));
                roofBlock.transform.localRotation = Quaternion.Euler(0, rotation, 0);


                wasBuilt = true; //Bug with double building temporary fix
            }
        }
    }

}
