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
    //[HideInInspector]
    //public GameObject wallTypePrefab;
    public int WidthRemaining;

    //public Direction wallDirection = Direction.XDirection;


    WallParameters wallParameters => GetComponent<WallParameters>();

    RandomGenerator random;


    [Header("Materials")]
    [SerializeField]
    Material[] wallmaterials;


    [SerializeField]
    public Material[] stoneMaterials;
    [SerializeField]
    public Material[] woodMaterials;


    public GameObject parent;


    private void Awake()
    {
        random = GetComponent<RandomGenerator>();
    }


    public void Initialize(int pWidthRemaining, GameObject prefab =null)
    {
        WidthRemaining = pWidthRemaining;
        //wallTypePrefab = prefab;
    }


    public void Generate() {
        DeleteGenerated();
        if (random != null)
        {
            random.ResetRandom();
        }
        Execute();
    }

    Material[] GetMaterial() {
        return wallmaterials;
    }


    protected override void Execute()
    {
        if (WidthRemaining <= 0)
        {
           
            

            return;
        }
        GameObject wallObject = SpawnPrefab(Root.GetComponent<WallParameters>().GetWallPiece());


     

        MeshRenderer[] meshRenderers = wallObject.GetComponentsInChildren<MeshRenderer>();


        parent = Root;

        RandomGenerator parentRandom = Root.GetComponent<RandomGenerator>();

        if (parent == null) {
            Debug.LogWarning(parent);
        }
        if (parentRandom == null)
        {
            Debug.LogWarning(parent);
        }
        

        var newMaterials = Root.GetComponent<Wall>()?.GetMaterial();
        var stoneMaterials = Root.GetComponent<Wall>().stoneMaterials;
        var woodMaterials = Root.GetComponent<Wall>().woodMaterials;

        foreach (var meshRenderer in meshRenderers) {
            var materials = meshRenderer.materials;


            for (int i = 0; i < materials.Length; i++) {
                //Debug.Log(materials[i].name);

                string materialName = materials[i].name;
                int materialIndex = 0;

                if (materialName.Contains("stone"))
                {
                    materialIndex = parentRandom.Next(0, stoneMaterials.Length);

                    materials[i] = stoneMaterials[materialIndex];
                }
                else if (materialName.Contains("wood"))
                {
                    materialIndex = parentRandom.Next(0, woodMaterials.Length);
                    materials[i] = woodMaterials[materialIndex];
                }
                else
                {
                    materials[i] = newMaterials[0];
                }
            }
            //if (materials.Length > 0)
            //{
            //    materials[0] = newMaterials[0];
            //}
            //if (materials.Length > 1)
            //{
            //    materials[1] = newMaterials[1];
            //}
            //if (materials.Length > 2)
            //{
            //    materials[2] = newMaterials[1];
            //}
            //if (materials.Length > 3)
            //{
            //    materials[3] = newMaterials[1];
            //}
            meshRenderer.materials = materials;
        }
        


       


        Wall wall = CreateSymbol<Wall>("WallSegment", new Vector3(0,0,1));
       
        wall.Initialize(WidthRemaining-1);
        wall.Generate(0.1f);

    }
}
