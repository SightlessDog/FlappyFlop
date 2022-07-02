using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tutorial:
//https://www.youtube.com/watch?v=YG-gIX_OvSE
[RequireComponent(typeof(MeshFilter))]
public class PolygonMeshGenerator : MonoBehaviour
{
    Mesh mesh;
    public Vector3[] vertices;
    public int[] triangles;

    public int polygonSides;
    public float polygonOuterRadius;
    public float centerInnerRadius;

    void Start()
    {
        mesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = mesh;
        //Modify number of polygon sides through the time
        InvokeRepeating("TransformPolygon", 1f, 1f);
    }

    void Update()
    {
        DrawHollow(polygonSides, polygonOuterRadius, centerInnerRadius);
        UpdateMesh();
    }

    /// <summary>
    /// Draw the hollow polygon based on the given number of
    /// sides, outerRadius and innerRadius
    /// </summary>
    /// <param name="sides"></param>
    /// <param name="outerRadius"></param>
    /// <param name="innerRadius"></param>
    private void DrawHollow(int sides, float outerRadius, float innerRadius)
    {
        //Defining List of vertices
        List<Vector3> pointsList = new List<Vector3>();
        List<Vector3> outerPoints = GetCircumferencePoints(sides, outerRadius);
        pointsList.AddRange(outerPoints);
        List<Vector3> innerPoints = GetCircumferencePoints(sides, innerRadius);
        pointsList.AddRange(innerPoints);
        
        vertices = pointsList.ToArray();
        
        //Draw Triangles based on the vertices
        triangles = DrawHollowTriangles(vertices);
    }

    /// <summary>
    /// List of triangles to create the hollow polygon based on the given sorted vertices
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    private int[] DrawHollowTriangles(Vector3[] points)
    {
        int sides = points.Length / 2;
        int triangleAmount = sides * 2;
        List<int> newTriangles = new List<int>();
        
        //Defining triangles
        for (int i = 0; i < sides; i++)
        {
            int outerIndex = i;
            int innerIndex = i + sides;
            
            newTriangles.Add(outerIndex);
            newTriangles.Add(innerIndex);
            newTriangles.Add((i + 1) % sides);

            newTriangles.Add(outerIndex);
            newTriangles.Add(sides + ((sides + i - 1) % sides));
            newTriangles.Add(outerIndex + sides);
        }

        return newTriangles.ToArray();
    }

    /// <summary>
    /// Return a list of circumference vertices of the polygon based on
    /// the number of points and radius we want to have
    /// </summary>
    /// <param name="pointNum"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    private List<Vector3> GetCircumferencePoints(int pointNum, float radius)
    {
        List<Vector3> points = new List<Vector3>();
        
        //distance between the points
        float circumferenceProgressPerStep = (float) 1 / pointNum;
        //TAU time constant
        float TAU = 2 * Mathf.PI;
        //radian value between 2 points
        float radianProgressPerStep = circumferenceProgressPerStep * TAU;

        //add calculated points to the list
        for (int i = 0; i < pointNum; i++)
        {
            //radian between first point and current point
            float currentRadian = radianProgressPerStep * i;
            //calculate the point on a xy-axis
            points.Add(new Vector3(Mathf.Cos(currentRadian) * radius, Mathf.Sin(currentRadian) * radius, 0));
        }

        return points;
    }

    /// <summary>
    /// Adjusting the sides of polygon through the time
    /// </summary>
    private void TransformPolygon()
    {
        polygonSides++;
        if (polygonSides >= 10)
        {
            polygonSides = 3;
        }
    }

    /// <summary>
    /// Update the mesh attributes
    /// </summary>
    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}