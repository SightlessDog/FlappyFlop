using UnityEngine;

//Tutorial:
//https://www.youtube.com/watch?v=zISwvdnR8JY
[RequireComponent(typeof(MeshFilter))]
public class BlockObstacleMeshGenerator : MonoBehaviour
{
    private Mesh mesh;

    [SerializeField] private float upDownFactor = 0.2f;
    [SerializeField] private float upDownSpeed = 5f;

    [SerializeField] private float leftFactor = 0.3f;
    [SerializeField] private float leftSpeed = 3f;
    [SerializeField] private float leftOffset = 2f;

    [SerializeField] private float strectFactor = -0.3f;
    [SerializeField] private float strectSpeed = 4f;
    
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        UpdateMesh();
    }
    
    void Update()
    {
        mesh.vertices = NewVertCube(Mathf.Sin(Time.realtimeSinceStartup * upDownSpeed) * upDownFactor,
            Mathf.Sin(Time.realtimeSinceStartup * leftSpeed + leftOffset) * leftFactor,
            Mathf.Sin(Time.realtimeSinceStartup * strectSpeed) * strectFactor
        );
    }

    private Vector3[] NewVertCube(float up = 0f, float left = 0f, float stretch = 0f)
    {
        return new Vector3[]
        {
            //Bottom
            new Vector3(-1, 0, 1),
            new Vector3(1, 0, 1),
            new Vector3(1, 0, -1),
            new Vector3(-1, 0, -1),

            //Top
            new Vector3(-1 - stretch + left, 2 + up, 1 + stretch),
            new Vector3(1 + stretch + left, 2 + up, 1 + stretch),
            new Vector3(1 + stretch + left, 2 + up, -1 - stretch),
            new Vector3(-1 - stretch + left, 2 + up, -1 - stretch),
        };
    }

    private int[] NewTrisCube()
    {
        return new int[]
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

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = NewVertCube();
        mesh.triangles = NewTrisCube();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}