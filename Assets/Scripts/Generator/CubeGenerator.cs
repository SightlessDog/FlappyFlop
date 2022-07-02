using UnityEngine;

//Tutorial:
//https://www.youtube.com/watch?v=zISwvdnR8JY
[RequireComponent(typeof(MeshFilter))]
public class CubeGenerator : MonoBehaviour
{
    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;

    [SerializeField] private float upDownFactor;
    [SerializeField] private float upDownSpeed;

    [SerializeField] private float leftFactor;
    [SerializeField] private float leftSpeed;
    [SerializeField] private float leftOffset;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        CreateCube();
        UpdateMesh();
    }

    void Update()
    {
        //Make the cube moving by updating the top vertices of the cube
        //Adjustable by speed/ factor/ offset inputs
        GetCubeVertices(Mathf.Sin(Time.realtimeSinceStartup * upDownSpeed) * upDownFactor,
            Mathf.Sin(Time.realtimeSinceStartup * leftSpeed + leftOffset) * leftFactor);
        mesh.vertices = vertices;
    }

    /// <summary>
    /// Create a static cube
    /// </summary>
    private void CreateCube()
    {
        GetCubeVertices(0f,0f);
        GetCubeTriangles();
    }

    /// <summary>
    /// Defining all vertices of a cube
    /// </summary>
    /// <param name="up"></param>
    /// <param name="left"></param>
    private void GetCubeVertices(float up, float left)
    {
        vertices = new Vector3[8];

        //bottom vertices are always static
        //Bottom
        vertices[0] = new Vector3(-1, 0, 1);
        vertices[1] = new Vector3(1, 0, 1);
        vertices[2] = new Vector3(1, 0, -1);
        vertices[3] = new Vector3(-1, 0, -1);

        //Adjusting top vertices of the cube based on the given input, which can make the cube moving during the time
        //Top
        vertices[4] = new Vector3(-1 + left, 2 + up, 1);
        vertices[5] = new Vector3(1 + left, 2 + up, 1);
        vertices[6] = new Vector3(1 + left, 2 + up, -1);
        vertices[7] = new Vector3(-1 + left, 2 + up, -1);
    }

    /// <summary>
    /// Defining all sides of a cube
    /// </summary>
    private void GetCubeTriangles()
    {
        //each side is created by 2 triangles with the corresponding vertices points below
        triangles = new int[]
        {
            //Bottom
            2, 1, 0,
            2, 0, 3,
            //Top
            4, 5, 6,
            4, 6, 7,
            //front
            2, 7, 6,
            2, 3, 7,
            //back
            4, 0, 1,
            4, 1, 5,
            //left
            0, 4, 3,
            3, 4, 7,
            //right
            1, 2, 6,
            1, 6, 5,
        };
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