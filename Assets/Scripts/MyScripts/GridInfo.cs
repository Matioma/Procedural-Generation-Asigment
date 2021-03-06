﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;



public class GridInfo : MonoBehaviour
{
    public int Width;
    public int Depth;




    //0 - nothing in the square
    //11 - 1 connection
    //12 - 1 conection rotate by 90Degress
    //13 - 1 conection rotate by 180Degrees
    //14 - 1 conection rotate by 270Degrees
    //21 - 2 connection
    //22- 2 conection rotate by 90Degress
    //23 - 2 conection rotate by 180Degrees
    //24 - 2 conection rotate by 270Degrees
    //31 - 3 connection
    //32 - 3 conection rotate by 90Degress
    //33 - 3 conection rotate by 180Degrees
    //34 - 3 conection rotate by 270Degrees
    [SerializeField]
    int[,] array;


   public  int[,] getArray() {
        return array;
    }


    public void Initialize(int width, int depth)
    {
        this.Width = width;
        this.Depth = depth;

        array = new int[depth,width];
        for (int i = 0; i < depth; i++) {
            for (int j = 0; j < width; j++) {
                array[i, j] = -1;
            }
        }
    }


    /// <summary>
    ///  fill the array with values Where are the walls
    /// </summary>
    /// <param name="array"></param>
    public void FillTheData(int[,] array) {
        for (int i = 0; i < Depth; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                this.array[i, j] = array[i, j];
            }
        }
        SetRightValues();
    }


    void SetRightValues() {
        for (int i = 0; i < Depth; i++) {
            for (int j = 0; j < Width; j++) {
                if (getValue(i,j) == -1) continue; //Check if the sector is connected to anything
                array[i,j] = ComputeValue(i,j);
            }
        }
    }

    int ComputeValue(int i,int j) {
        //0001 - >1 - Conection Left
        //0010 -> 2 -> Conection Bottom
        //0100 -> 4 -> Connection RIght
        //1000 -> 8 -> Connection Top
        

        //0000 0001  -> 1 connection top
        //0000 0010 -> conetection top-right
        //0000 0100 -> connection right
        //0000 1000 -> coneection bottom right
        //0001 0000 -> connection bottom
        //0010 0000 -> connection bottom -Left
        //0100 0000 -> left
        //1000 0000 -> top left



        int connectionType = 0;

        //SomethingToTop;
        if (inRange(i + 1, j) && getValue(i + 1, j) > -1)
        {
            connectionType += 1;

           
            //connectionType += 8;
        }

        //Something top-right
        if (inRange(i + 1, j+1) && getValue(i + 1, j+1) > -1)
        {
           
            connectionType += 2;


            //connectionType += 8;
        }

        //SOmething right
        if (inRange(i, j + 1) && getValue(i, j + 1) > -1)
        {
            connectionType += 4;
           
        }
        //SOmething bottom right
        if (inRange(i - 1, j+1) && getValue(i - 1, j+1) > -1)
        {
            connectionType += 8;
        }

        //Somehting Down;
        if (inRange(i - 1, j) && getValue(i - 1, j) > -1)
        {
            connectionType += 16;
        }

        //Something Down-left
        if (inRange(i - 1, j-1) && getValue(i - 1, j-1) > -1)
        {
            connectionType += 32;
        }
        //Something LEft
        if (inRange(i, j - 1) && getValue(i, j - 1) > -1)
        {
            connectionType += 64;
          
        }

        //Something Left-top
        if (inRange(i+1, j - 1) && getValue(i+1, j - 1) > -1)
        {
            connectionType += 128;

        }



        return connectionType;
    }

    int numberOfContacts() { 
        //int
                return 0;
    }

    public int getValue(int depthIndex, int widthIndex) {
        if (inRange(depthIndex, widthIndex))
        {
            return array[depthIndex, widthIndex];
        }
        return 0;
        
    }

    //Checks if index of the cell is in range
    bool inRange(int row, int column)
    {
        if (array==null || array.Length == 0) {
            return false;
        }
        if (row >= 0 && row < Depth && column >= 0 && column < Width) {
            return true;
        }
        return false;
    }
}
