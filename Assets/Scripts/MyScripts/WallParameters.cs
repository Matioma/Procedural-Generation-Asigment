
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WallParameters : MonoBehaviour
{
    public GameObject[] walls;
    public GameObject[] doors;
    public GameObject[] windows;

    public bool hasDoor = false;

    [SerializeField]
    Pattern wallPattern;
    public GameObject GetWallPiece() {
        if (walls ==null && walls.Length == 0) {
            Debug.LogError("Wall types array is empty");
            return null;
        }
        int randomIndex =0;

        if (wallPattern.Length > 0)
        {
            randomIndex = wallPattern.GetValue();
        }
        else {
            randomIndex = GetComponent<RandomGenerator>().Next(0, walls.Length);
        }
        return walls[randomIndex];
    }
}
