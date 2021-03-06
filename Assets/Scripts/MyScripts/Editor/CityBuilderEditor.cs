﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(CityBuilder),true)]
public class CityEditor : BuilderGenerateButtons
{
    public override void OnInspectorGUI()
    {
        var cityBuilder = target as CityBuilder;
        base.OnInspectorGUI();

        if (GUILayout.Button("New Street Layout"))
        {
            cityBuilder.CreatePoints();
        }
    }

    protected virtual void OnSceneGUI()
    {
        CityBuilder city = (CityBuilder)target;

        Handles.color = new Color(0, 0, 0, 0.5f);


        Vector3[] globalPositions = city.CityShape.ToArray();
     
        

        for (int i = 0; i < globalPositions.Length; i++) {
            globalPositions[i] = city.transform.position + globalPositions[i];
        }
        Handles.DrawAAConvexPolygon(globalPositions);


        Handles.color = new Color(100, 100, 100, 0.7f);
       
        Handles.DrawAAPolyLine(10,globalPositions);


        //Handles.preselectionColor = new Color(127, 127, 127);
        for (int i = 0; i < city.CityShape.Count; i++)
        {
            city.CityShape[i] = Handles.PositionHandle(city.CityShape[i]+ city.transform.position, Quaternion.identity) - city.transform.position;
        }

        Handles.color = new Color(0, 255, 0);

        // Draw Streets Segments 
        //
        for (int i = 0; i < city.finalEdge.Count-1; i++)
        {
            Handles.DrawLine(city.finalEdge[i].PointStart.Position, city.finalEdge[i].PointEnd.Position);
        }


        Handles.color = Handles.xAxisColor;

        // Draw Streets grid  handles
        for (int i = 0; i < city.finalEdge.Count; i++)
        {
            Vector3 positionStart = city.finalEdge[i].PointStart.Position;
            Vector3 positionEnd = city.finalEdge[i].PointEnd.Position;

            city.finalEdge[i].PointStart.Position = Handles.PositionHandle(new Vector3(positionStart.x, city.transform.position.y, positionStart.z), Quaternion.identity);
            city.finalEdge[i].PointEnd.Position = Handles.PositionHandle(new Vector3(positionEnd.x, city.transform.position.y, positionEnd.z), Quaternion.identity);
        }




    }
}
