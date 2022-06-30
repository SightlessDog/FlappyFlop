using UnityEngine;

// Tutorial:
// https://www.youtube.com/watch?v=eJEpeUH1EMg
// https://www.youtube.com/watch?v=64NblGkAabk
// https://www.youtube.com/watch?v=lNyZ9K71Vhc
[RequireComponent(typeof(MeshFilter))]
public class TerrainMeshGenerator : MonoBehaviour
{
    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;

    //Size of the mesh to generate vertices
    [SerializeField] private int xSize;
    [SerializeField] private int zSize;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    /// <summary>
    /// Create Shape including vertices, triangles, and uv mapping
    /// </summary>
    private void CreateShape()
    {
        CreateVertices();
        CreateTriangles();
        CreateUV();
    }

    /// <summary>
    /// Defining vertices based on the given size
    /// </summary>
    private void CreateVertices()
    {
        //vertices = length + 1
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        //looping through the array of vertices
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                //Using PerlinNoise to make the terrain rough
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 20f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }
    }

    /// <summary>
    /// Defining triangles based on the generated vertices
    /// </summary>
    private void CreateTriangles()
    {
        //calculate number of triangles based on the given size
        triangles = new int[xSize * zSize * 6];

        for (int vert = 0, tris = 0, z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                //Defining 2 triangles to create a square
                //Each triangle would require 3 vertices
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                //After having a square generated, we will continue the loop
                //and shifting it from the left to the right
                //bottom to the top to generate a whole mesh based on the created number of vertices
                vert++;
                tris += 6;
            }

            vert++;
        }
    }

    private void CreateUV()
    {
        uvs = new Vector2[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                uvs[i] = new Vector2((float) x / xSize, (float) z / zSize);
                i++;
            }
        }
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}