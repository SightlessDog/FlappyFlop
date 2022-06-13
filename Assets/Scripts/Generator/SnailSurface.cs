using UnityEngine;
using static UnityEngine.Mathf; 

public class SnailSurface : MeshFunction
{
    public Vector2 UMinMax => new Vector2(-1 * PI, 1 * PI);
    public Vector2 VMinMax => new Vector2(-1 * PI, 1 * PI);
    public Vector2Int Subdivisions => new Vector2Int(100, 100);

    public Vector3 Vertex(float u, float v) => new Vector3(
        u * Cos(v) * Sin(v),
        u * Cos(u) * Cos(v),
        -u * Sin(v)
    );
}