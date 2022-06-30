using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tutorial:
//https://www.youtube.com/watch?v=YG-gIX_OvSE
[RequireComponent(typeof(MeshFilter))]
public class PolygonMeshGenerator : MonoBehaviour
{
    Mesh mesh;
    public Vector3[] polygonPoints;
    public int[] polygonTriangles;

    public int polygonSides;
    public float polygonRadius;
    public float centerRadius;

    void Start()
    {
        mesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = mesh;
        InvokeRepeating("TransformPolygon", 1f, 1f);
    }

    void Update()
    {
        DrawHollow(polygonSides, polygonRadius, centerRadius);
    }

    void DrawHollow(int sides, float outerRadius, float innerRadius)
    {
        List<Vector3> pointsList = new List<Vector3>();
        List<Vector3> outerPoints = GetCircumferencePoints(sides, outerRadius);
        pointsList.AddRange(outerPoints);
        List<Vector3> innerPoints = GetCircumferencePoints(sides, innerRadius);
        pointsList.AddRange(innerPoints);
        
        polygonPoints = pointsList.ToArray();

        polygonTriangles = DrawHollowTrianges(polygonPoints);

        mesh.Clear();
        mesh.vertices = polygonPoints;
        mesh.triangles = polygonTriangles;
    }

    int[] DrawHollowTrianges(Vector3[] points)
    {
        int sides = points.Length / 2;
        int triangleAmount = sides * 2;

        List<int> newTriangles = new List<int>();
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

    List<Vector3> GetCircumferencePoints(int sides, float radius)
    {
        List<Vector3> points = new List<Vector3>();
        float circumferenceProfessPerStep = (float) 1 / sides;
        float TAU = 2 * Mathf.PI;
        float radianProgressPerStep = circumferenceProfessPerStep * TAU;

        for (int i = 0; i < sides; i++)
        {
            float currentRadian = radianProgressPerStep * i;
            points.Add(new Vector3(Mathf.Cos(currentRadian) * radius, Mathf.Sin(currentRadian) * radius, 0));
        }

        return points;
    }

    void TransformPolygon()
    {
        polygonSides++;
        if (polygonSides >= 10)
        {
            polygonSides = 3;
        }
    }
}