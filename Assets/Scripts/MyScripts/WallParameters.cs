
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WallParameters : MonoBehaviour
{
    public GameObject[] walls;
    public GameObject[] doors;
    public GameObject[] windows;

    public bool hasDoor = false;


    public GameObject GetWallPiece() {
        if (walls ==null && walls.Length == 0) {
            Debug.LogError("Wall types array is empty");
            return null;
        }
        int randomIndex = Random.Range(0, walls.Length-1);
        return walls[randomIndex];
    }
}
