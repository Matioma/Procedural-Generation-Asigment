using Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System;

[RequireComponent(typeof(RandomGenerator),typeof(RoadBuilder))]
public class CityBuilder : Builder
{

    [SerializeField]
    StreetParameters streetParameters;

    [SerializeField]
    int numberOfSplitsOfTheRoad;


    [SerializeField]
    public List<Vector3> CityShape;


    [SerializeField]
    BuildingParameters buildingParameters;


    //[SerializeField]
    //bool roadsOutsideTheArea;
    //[SerializeField]
    //bool roadsInsideTheArea;

    [SerializeField]
    int numberOfPoints = 5;


    List<Edge> edges = new List<Edge>();

    [HideInInspector]
    public List<Point> points = new List<Point>();

    //[HideInInspector]
    public List<Edge> finalEdge = new List<Edge>();

    private void Awake()
    {
        base.Awake();
        
    }


    //Creates Points sorts edges
    public void CreatePoints() {
        RandomGenerator random= GetComponent<RandomGenerator>();

        edges.Clear();
        points.Clear();
        finalEdge.Clear();


        for (int i = 0; i < CityShape.Count; i++) {
            Point point = new Point(CityShape[i] + transform.position);
            points.Add(point);
        }


        Vector3[] boundries = BoundingBox(CityShape);
       

        for (int i = 0; i < numberOfPoints; i++) {
            Vector3 pointToCheck;

            pointToCheck = new Vector3(random.Next((int)boundries[0].x, (int)boundries[1].x), 0, random.Next((int)boundries[2].z, (int)boundries[3].z));
            Point point = new Point(pointToCheck);
            points.Add(point);
        }

        for (int i = 0; i < points.Count- 1; i++) {
            for (int j = i+1; j < points.Count; j++) {
                Edge newEdge = new Edge(points[i], points[j]);
                if (newEdge.Length > 0) {
                    edges.Add(newEdge);
                }
            }
        }
        edges.Sort((x, y) => x.Length.CompareTo(y.Length));

        Krukal();
    }

    Vector3[] BoundingBox(List<Vector3> polygon) {
        var boundries = new Vector3[4];

        Vector3 minX = polygon[0];
        Vector3 maxX = polygon[0];


        Vector3 minZ = polygon[0];
        Vector3 maxZ = polygon[0];


        for (int i = 1; i < polygon.Count; i++) {
            if (polygon[i].x > maxX.x)
            {
                maxX = polygon[i];
            }
            if (polygon[i].x < minX.x) {
                minX = polygon[i];
            }

            if (polygon[i].z > maxZ.z)
            {
                maxZ = polygon[i];
            }
            if (polygon[i].x < minZ.z)
            {
                minZ = polygon[i];
            }
        }

        boundries[0] = minX + transform.position;
        boundries[1] = maxX + transform.position;
        boundries[2] = minZ + transform.position;
        boundries[3] = maxZ + transform.position;

        Debug.Log(boundries[0]);
        Debug.Log(boundries[1]);
        Debug.Log(boundries[2]);
        Debug.Log(boundries[3]);

        return boundries;
    }

    void Krukal() {

        int edgeCount = 0;

        int i = 0;
        while (edgeCount < points.Count -1)
        {
            if(edges[i].PointStart.parent != edges[i].PointEnd.parent)
            {
                edges[i].PointEnd.SetParent(edges[i].PointStart.parent);
                finalEdge.Add(edges[i]);
                edgeCount++;
            }

            i++;
        }
    }

    public override void  Generate()
    {
        base.Generate();
        BuildCity();
    }

    void BuildCity() {
        RemoveChildren();
        buildRoad();
    }
    void RemoveChildren() {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void buildRoad() {
        if (CityShape.Count != 0)
        {
            for (int i = 0; i < CityShape.Count; i++)
            {
                streetParameters.startPosition = transform.position + CityShape[i];
                if (i < CityShape.Count-1)
                {
                    streetParameters.endPosition = transform.position + CityShape[i + 1];
                }
                else
                {
                    streetParameters.endPosition = transform.position + CityShape[0];
                }

                streetParameters.streetLength = (streetParameters.startPosition - streetParameters.endPosition).magnitude;
                GetComponent<RoadBuilder>()?.SpawnRoad(streetParameters, buildingParameters, numberOfSplitsOfTheRoad);
            }
        }

        if (finalEdge.Count > 0) {
            for (int i = 0; i < finalEdge.Count; i++) {
                streetParameters.startPosition = finalEdge[i].PointStart.Position;
                streetParameters.endPosition = finalEdge[i].PointEnd.Position;


                streetParameters.streetLength = (streetParameters.startPosition - streetParameters.endPosition).magnitude;
                GetComponent<RoadBuilder>()?.SpawnRoad(streetParameters, buildingParameters, numberOfSplitsOfTheRoad);
            }
           


        }

    }
}



public class EdgeSorter : IComparer
{
    int IComparer.Compare(object a, object b)
    {
        Edge c1 = (Edge)a;
        Edge c2 = (Edge)b;
        if (c1.Length > c2.Length)
            return 1;
        if (c1.Length < c2.Length)
            return -1;
        else
            return 0;
    }
}

[System.Serializable]
public class Edge
{
    [SerializeField]
    public Point PointStart;
    [SerializeField]
    public Point PointEnd;


    public float Length {
        get { return (PointEnd.Position - PointStart.Position).magnitude; }
    }

   
    public Edge(Point start, Point end) {
        PointStart = start;
        PointEnd = end;
    }

}

[System.Serializable]
public class Point {
    static int pointsCreated = 0;

    [SerializeField]
    Vector3 position;
    public Point parent;
    public int id;

    List<Point> Children = new List<Point>();

    public void AddChildren(List<Point> points) {
        //Make sure that all chiled have this parrent
        for (int i = 0; i < points.Count; i++) {
            Children.Add(points[i]);
      
        }

    }

    string ToString() {
        return "Parent: "+  parent.Position + "; id = " + parent.id + "----\n" + "Point:" + Position + "; id = " + parent.id;
    }


    public Vector3 Position {
        get { return position; }
        set { position = value; }

    }

    public void SetParent(Point newParent)
    {
        parent = newParent;
        for (int i = 0; i < Children.Count; i++) {
            Children[i].parent = newParent;
        }
        parent.AddChildren(Children);

        Children.Clear();
    }

    public Point(Vector3 position) {
        this.position = position;
        
        pointsCreated++;
        id = pointsCreated;

        parent = this;
    }

}

