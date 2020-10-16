using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(CityBuilder),true)]
public class CityEditor : BuilderGenerateButtons
{
}


[CustomEditor(typeof(Road), true)]
public class RoadEditor : BuilderGenerateButtons
{
}
