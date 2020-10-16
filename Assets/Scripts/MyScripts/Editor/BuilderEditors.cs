using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;

[CustomEditor(typeof(CityBuilder),true)]
public class CityEditor : BuilderGenerateButtons
{
}


[CustomEditor(typeof(Road), true)]
public class RoadEditor : BuilderGenerateButtons
{
    Tool LastTool = Tool.None;
    void OnEnable()
    {
        LastTool = Tools.current;
        Tools.current = Tool.None;
    }

    void OnDisable()
    {
        Tools.current = LastTool;
    }


    protected virtual void OnSceneGUI()
    {
        Road road = (Road)target;


        EditorGUI.BeginChangeCheck();
        //Vector3 startPosition = target.startPosition;
        Vector3 start = Handles.PositionHandle(road.startPosition, Quaternion.identity);
        Vector3 end = Handles.PositionHandle(road.endPosition, Quaternion.identity);
        //Vector3 newTargetPosition = Handles.PositionHandle();
        if (EditorGUI.EndChangeCheck())
        {
            road.startPosition = start;
            road.endPosition = end;
            //Debug.lo
            //Undo.RecordObject(example, "Change Look At Target Position");
            //example.targetPosition = newTargetPosition;
            //example.Update();
        }
    }
}
