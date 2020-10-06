using Demo;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public enum Direction{ 
    XDirection,
    NegativeXDirection,
    YDirection,
    NegativeYDirection,
    ZDirection,
    NegativeZDirection
}


[RequireComponent(typeof(WallParameters))]
public class Wall : Shape
{
    public GameObject wallTypePrefab;
    public int WidthRemaining;

    public Direction wallDirection = Direction.XDirection;


    WallParameters wallParameters => GetComponent<WallParameters>();



    public void Initialize(int pWidthRemaining, GameObject prefab)
    {
        WidthRemaining = pWidthRemaining;
        wallTypePrefab = prefab;
    }


    protected override void Execute()
    {
        if (WidthRemaining <= 0)
        {
            return;
        }
        GameObject wallObject = SpawnPrefab(Root.GetComponent<WallParameters>().GetWallPiece());
        
     
        //GameObject box =SpawnPrefab(wallTypePrefab);

        
        Wall wall = CreateSymbol<Wall>("WallSegment", new Vector3(0,0,1));
        //setDirection(wall.transform, wallDirection);
        wall.Initialize(WidthRemaining-1, wallTypePrefab);
        wall.Generate(0.1f);

    }


    private void setDirection(Transform prefabRef,Direction direction)
    {
        switch (direction) {
            case Direction.NegativeXDirection:
                transform.localRotation = Quaternion.Euler(0, -90, 0);
                break;
            case Direction.XDirection:
                transform.localRotation = Quaternion.Euler(0,-180,0);
                break;
            case Direction.NegativeZDirection:
                transform.localRotation = Quaternion.Euler(0, -180, 0);
                break;
            case Direction.ZDirection:
                transform.localRotation = Quaternion.Euler(0, -270, 0);
                break;

        }
    }
}
