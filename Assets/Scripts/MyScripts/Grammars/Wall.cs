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

    //public Direction wallDirection = Direction.XDirection;


    WallParameters wallParameters => GetComponent<WallParameters>();

    RandomGenerator random;

   

    private void Awake()
    {
        random = GetComponent<RandomGenerator>();
    }


    public void Initialize(int pWidthRemaining, GameObject prefab)
    {
        WidthRemaining = pWidthRemaining;
        wallTypePrefab = prefab;
    }


    public void Generate() {
        DeleteGenerated();
        if (random != null)
        {
            random.ResetRandom();
        }
        Execute();
    }

    protected override void Execute()
    {
        if (WidthRemaining <= 0)
        {
            return;
        }
        GameObject wallObject = SpawnPrefab(Root.GetComponent<WallParameters>().GetWallPiece());
        
        
        Wall wall = CreateSymbol<Wall>("WallSegment", new Vector3(0,0,1));
        wall.Initialize(WidthRemaining-1, wallTypePrefab);
        wall.Generate(0.1f);

    }
}
