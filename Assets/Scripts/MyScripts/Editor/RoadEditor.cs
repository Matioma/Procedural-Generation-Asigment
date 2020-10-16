using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
        Vector3 start = Handles.PositionHandle(road.streetParameters.startPosition, Quaternion.identity);
        Vector3 end = Handles.PositionHandle(road.streetParameters.endPosition, Quaternion.identity);
     
        if (EditorGUI.EndChangeCheck())
        {
            road.streetParameters.startPosition = start;
            road.streetParameters.endPosition = end;
        }
    }
}
