using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extended in the different implementations of the function
/// See: Helice.cs for example 
/// </summary>
public interface MeshFunction
{
    Vector2 UMinMax { get; }
    Vector2 VMinMax { get; }
    Vector2Int Subdivisions { get; }
    Vector3 Vertex(float u, float v);
}

public enum MeshType
{
    Plane, 
    Snail, 
    Torus,
    Sphere, 
    Helice
}

public class MeshGenerator : MonoBehaviour
{
    public static MeshGenerator Instance;
    [SerializeField] private Mesh generatedMesh;
    [SerializeField] private MeshType meshType;

    private void Start()
    {
        GenerateMesh();
    }

    void Awake()
    {
        Instance = this;
    }

    public void GenerateMesh()
    {
        GenerateMeshes(SelectMeshFunction());
    }

    MeshFunction SelectMeshFunction() => meshType switch
    {
        MeshType.Plane => new Plane(),
        MeshType.Snail => new SnailSurface(),
        MeshType.Torus => new TwistedTorus(),
        MeshType.Sphere => new TwistedSphere(),
        MeshType.Helice => new Helice(),
        _ => throw new ArgumentOutOfRangeException(nameof(meshType))
    };

    public void GenerateMeshes(MeshFunction meshFunction)
    {
        generatedMesh = new Mesh();

        var subdivisions = meshFunction.Subdivisions;
        // If we have M,N subdivisions -> we have M+1 vertices in X direction and N + 1 vertices in Y direction
        var vertexSize = subdivisions + new Vector2Int(1, 1);

        var vertices = new Vector3[vertexSize.x * vertexSize.y];
        var uvs = new Vector2[vertices.Length];

        var uDelta = meshFunction.UMinMax.y - meshFunction.UMinMax.x;
        var vDelta = meshFunction.VMinMax.y - meshFunction.VMinMax.x;
        
        // Generate the points
        for (var y = 0; y < vertexSize.y; y++)
        {
            // Normalize the y coordinate as v
            var v = (1f / subdivisions.y) * y;

            for (var x = 0; x < vertexSize.x; x++)
            {
                // Normalize the x coordinate as u
                var u = (1f / subdivisions.x) * x;
                var scaledUv = new Vector2(u * uDelta - meshFunction.UMinMax.x, v * vDelta - meshFunction.VMinMax.x);
                var vertex = meshFunction.Vertex(scaledUv.x, scaledUv.y);

                var arrayIndex = x + y * vertexSize.x;
                vertices[arrayIndex] = vertex;
                uvs[arrayIndex] = new Vector2(u, v);
            }
        }

        // Connect the points and generate the triangles
        var triangles = new int[subdivisions.x * subdivisions.y * 6];
        for (var i = 0; i < subdivisions.x * subdivisions.y; i += 1)
        {
            var triangleIndex = (i % subdivisions.x) + (i / subdivisions.x) * vertexSize.x;
            var indexer = i * 6;

            triangles[indexer + 0] = triangleIndex;
            triangles[indexer + 1] = triangleIndex + subdivisions.x + 1;
            triangles[indexer + 2] = triangleIndex + 1;
            
            triangles[indexer + 3] = triangleIndex + 1;
            triangles[indexer + 4] = triangleIndex + subdivisions.x + 1;
            triangles[indexer + 5] = triangleIndex + subdivisions.x + 2;
        }

        generatedMesh.vertices = vertices;
        generatedMesh.uv = uvs;
        generatedMesh.triangles = triangles;
        
        generatedMesh.RecalculateBounds();
        generatedMesh.RecalculateNormals();
        generatedMesh.RecalculateTangents();
        
        // Display the mesh
        GetComponent<MeshFilter>().mesh = generatedMesh;
    }
}
