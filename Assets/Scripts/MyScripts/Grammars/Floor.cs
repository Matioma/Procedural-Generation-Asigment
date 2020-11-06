using Demo;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent (typeof(RandomGenerator))]
public class Floor : Shape
{
    public GameObject wallPrefab;

    public int width;
    public int depth;

    public int rotationsLeft = 3;

    public int[,] floorPlan;

    Vector2Int index = new Vector2Int(0, 0);
    MyDirection mydirection = new MyDirection(new Vector2Int(0, 1));


    RandomWithSeed random;

   

    
    
    private void Start()
    {
        //GetComponent<Buildings>()?.Trigger();

        //Generate();
     
    }





    public void Initialize(int pRotationsLeft, GameObject prefab)
    {
        rotationsLeft = pRotationsLeft;
        wallPrefab = prefab;
    }

    public void Initialize(int[,] floorPlan)
    {
        this.floorPlan = floorPlan;


        this.width = floorPlan.GetLength(0);
        this.depth = floorPlan.GetLength(1);

        //GetComponent<Buildings>()?.Trigger();
        Generate();
    }
    
 


    protected override void Execute()
    {
        GameObject wall;

        do
        {
            wall = SpawnPrefab(wallPrefab);
            wall.transform.localPosition = new Vector3(index.x, 0, index.y);
            wall.transform.localRotation = Quaternion.Euler(0, mydirection.directionRotation, 0);

            Wall wallComponent = wall.GetComponent<Wall>();
          

            wallComponent.WidthRemaining = computeWallWidth();
            wall.GetComponent<RandomGenerator>().seed = GetComponent<RandomGenerator>().Next(0,int.MaxValue);

            //if (index.x != 0 || index.y != 0) {
                wallComponent.Generate();
            //}
           


        } while (index.x != 0 || index.y != 0) ;
    }

    bool indexInBounds(Vector2Int index) {
        if (floorPlan == null || floorPlan.Length == 0)
        {
            return false;
        }
        if (index.x >= 0 && index.x < floorPlan.GetLength(0) && index.y >= 0 && index.y < floorPlan.GetLength(1))
        {
            return true;
        }
        return false;
    }

    bool canIreachNextIndex()
    {
        Vector2Int nextPosition = index +mydirection.direction;
        if (indexInBounds(nextPosition)) {

            //Detect turn
            MyDirection directionCopyLeft = mydirection.getCopy();
            MyDirection directionCopyRight = mydirection.getCopy();

            directionCopyLeft.directionTurnLeft();
            directionCopyRight.directionTurnRight();

            Vector2Int forwardLeft = mydirection.direction + directionCopyLeft.direction;
            Vector2Int forwardRight = mydirection.direction + directionCopyRight.direction;

            Vector2Int nextPositionForwardLeft = index + forwardLeft;
            Vector2Int nextPositionForwardRight = index + forwardRight;
            if (indexInBounds(nextPositionForwardLeft) && indexInBounds(nextPositionForwardRight)) {
                if (floorPlan[nextPositionForwardLeft.x, nextPositionForwardLeft.y] == 1 && floorPlan[nextPositionForwardRight.x, nextPositionForwardRight.y] == 1) {
                    return false;
                }
            }


            if (floorPlan[nextPosition.x, nextPosition.y] == 1) {
                return true;
            }
        }

        //index 
        return false;
    }

    void moveToNextIndex() {
        if (!canIreachNextIndex()) return;
        index += mydirection.direction;
        //Debug.Log(index);
    }

    int computeWallWidth() {
        int wallWidth = 1;
        while (canIreachNextIndex()) {
            moveToNextIndex();
            wallWidth++;
        }
        turn();

        return wallWidth;
    }


    //Decide to turn left or right
    void turn() {
        MyDirection directionCopyLeft = mydirection.getCopy();
        MyDirection directionCopyRight = mydirection.getCopy();

        directionCopyLeft.directionTurnLeft();
        directionCopyRight.directionTurnRight();

        Vector2Int positionAtLeft = index + directionCopyLeft.direction;
        Vector2Int positionAtRight = index + directionCopyRight.direction;

        Vector2Int positionInFront = index + mydirection.direction;


        
        //In front is 0
        if (indexInBounds(positionInFront))
        {
            if (floorPlan[positionInFront.x, positionInFront.y] == 0)
            {
                if (indexInBounds(positionAtLeft))
                {
                    if (floorPlan[positionAtLeft.x, positionAtLeft.y] == 1)
                    {
                        mydirection.directionTurnLeft();
                        return;
                    }
                    //mydirection.directionTurnRight();
                }
                if (indexInBounds(positionAtRight))
                {
                    if (floorPlan[positionAtRight.x, positionAtRight.y] == 1)
                    {
                        mydirection.directionTurnRight();
                        return;
                    }
                }
            }
        }
        //In front is out of the edge
        else {
            mydirection.directionTurnRight();
            return;
        }
       

        if (floorPlan[positionInFront.x, positionInFront.y] == 1) {

            if (indexInBounds(positionAtLeft))
            {
                if (floorPlan[positionAtLeft.x, positionAtLeft.y] == 0)
                {
                    index += mydirection.direction;
                    mydirection.directionTurnLeft();
                    index += mydirection.direction;
                    return;
                }
            }
            if (indexInBounds(positionAtRight))
            {
                if (floorPlan[positionAtRight.x, positionAtRight.y] == 0)
                {
                    mydirection.directionTurnRight();
                    
                    return;
                }
            }

        }


        //if (indexInBounds(positionAtLeft) && indexInBounds(positionAtRight))
        //{
        //    if (floorPlan[positionAtLeft.x, positionAtLeft.y] == 1 && floorPlan[positionAtRight.x, positionAtRight.y] == 1)
        //    {
        //        //return false;
        //    }
        //}

        mydirection.directionTurnRight();
        //directionTurnRight();
    }


}



public class MyDirection {

    public Vector2Int direction;
    public float directionRotation = 0;

    public MyDirection(Vector2Int direction, float rotation = 0) {
        this.direction = direction;
        this.directionRotation = rotation;
    }
    public void directionTurnRight()
    {
        directionRotation += 90;
        direction.x = (int)Mathf.Sin(Mathf.Deg2Rad * directionRotation);
        direction.y = (int)Mathf.Cos(Mathf.Deg2Rad * directionRotation);

    }
    public void directionTurnLeft()
    {
        directionRotation -= 90;
        direction.x = (int)Mathf.Sin(Mathf.Deg2Rad * directionRotation);
        direction.y = (int)Mathf.Cos(Mathf.Deg2Rad * directionRotation);
    }

    public MyDirection getCopy() {
        return new MyDirection(new Vector2Int(direction.x, direction.y), directionRotation);
    
    }
}
