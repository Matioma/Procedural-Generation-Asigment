
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WallParameters : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject[] walls;
    public GameObject[] windows;

    public bool hasDoor = false;

    [SerializeField]
    Pattern doorWallWindowPattern;


    [SerializeField]
    Pattern doorsPattern;
    [SerializeField]
    Pattern wallsPattern;
    [SerializeField]
    Pattern windowsPattern;

   
    RandomGenerator random;







    private void Awake()
    {
        
    }
    public GameObject GetWallPiece() {
        if(random ==null)
            random = GetComponent<RandomGenerator>();

        if (walls ==null && walls.Length == 0) {
            Debug.LogError("Wall types array is empty");
            return null;
        }
        int randomIndex =0;
        int randomObjectTypeIndex = 0;



        if (doorWallWindowPattern.Length > 0)
        {
            randomObjectTypeIndex = doorWallWindowPattern.GetValue();
        }
        else {
            randomObjectTypeIndex = random.Next(1, 3);
        }
        switch (randomObjectTypeIndex)
        {
            case 0:
                if (doorsPattern.Length > 0)
                {
                    randomIndex = doorsPattern.GetValue();
                }
                else {
                    randomIndex = random.Next(0, doors.Length);
                }
                return doors[randomIndex];
                break;
            case 1:
                if (wallsPattern.Length > 0)
                {
                    randomIndex = wallsPattern.GetValue();
                }
                else {
                    randomIndex = random.Next(0, walls.Length);
                }
                return walls[randomIndex];
                break;
            case 2:
                if (windowsPattern.Length > 0)
                {
                    randomIndex = windowsPattern.GetValue();
                }
                else {
                    randomIndex = random.Next(0, windows.Length);
                }
                return windows[randomIndex];
                break;
        }

        //randomIndex = random.Next(0, walls.Length);


        return null;

        return walls[randomIndex];
    }
}
