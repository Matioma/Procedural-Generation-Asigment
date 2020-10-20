using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(CityBuilder),true)]
public class CityEditor : BuilderGenerateButtons
{


    protected virtual void OnSceneGUI()
    {
        CityBuilder city = (CityBuilder)target;

        Handles.color = new Color(127, 127, 127, 0.5f);


        //Compute global position of Polygon Verticies
        Vector3[] globalPositions = city.CityShape.ToArray();
     
        


        for (int i = 0; i < globalPositions.Length; i++) {
            globalPositions[i] = city.transform.position + globalPositions[i];
        }
        Handles.DrawAAConvexPolygon(globalPositions);


        Handles.color = new Color(0, 255, 0, 0.5f);
        Handles.DrawAAPolyLine(10,globalPositions);



        for (int i = 0; i < city.CityShape.Count; i++)
        {
            //Vector3 localPosition = city.transform.position + city.CityShape[i];
            city.CityShape[i] = Handles.PositionHandle(city.CityShape[i]+ city.transform.position, Quaternion.identity) - city.transform.position;
        }



    }
}
