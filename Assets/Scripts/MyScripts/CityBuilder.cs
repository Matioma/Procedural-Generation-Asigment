using Demo;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting;
using UnityEngine;
using UnityEngine.UIElements;

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

    List<Point> points = new List<Point>();

    List<Edge> finalEdge = new List<Edge>();


    bool debug=false;

    private void Awake()
    {
        base.Awake();
        CreatePoints();
        //Generate();
    }


    //Creates Points sorts edges
    void CreatePoints() {
        RandomGenerator random= GetComponent<RandomGenerator>();

        for (int i = 0; i < numberOfPoints; i++) {
            Point point = new Point(new Vector3(Random.Range(0,100),0, Random.Range(0,100)));

            //Debug.Log(point.parent);
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

        debug = true;
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
    }


    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.green;

      
        if (points.Count > 0) {
            
            for (int i = 0; i < points.Count; i++)
            {
                Gizmos.DrawSphere(points[i].Position, 1);
            }
        }


        Gizmos.color = Color.red;
        if (finalEdge.Count > 0 && debug)
        {
            for (int i = 0; i < finalEdge.Count; i++)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(finalEdge[i].PointStart.Position, 2.0f);
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(finalEdge[i].PointStart.Position, 2.0f);

                Gizmos.color = new Color(255, 0,255);
                Gizmos.DrawLine(finalEdge[i].PointStart.Position, finalEdge[i].PointEnd.Position);
            }
        }




    }
}



class EdgeSorter : IComparer
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
class Edge
{
    public Point PointStart;
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
class Point {
    static int pointsCreated = 0;

    Vector3 position;
    public Point parent;
    public int id;

    List<Point> Children = new List<Point>();

    public void AddChildren(List<Point> points) {
        //Make sure that all chiled have this parrent
        for (int i = 0; i < points.Count; i++) {
            Children.Add(points[i]);
            
            
            //points[i].parent =parent;


            //Debug.Log(points[i].ToString());
            //Debug.Log(parent.Position);
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

        //Debug.Log(parent.Children.Count);

        Children.Clear();
    }

    public Point(Vector3 position) {
        //this.position = new Vector3(position.x,position.y,position.z);
        this.position = position;
        
        pointsCreated++;
        id = pointsCreated;

        parent = this;
        //Children.Add(this);
    }

}
