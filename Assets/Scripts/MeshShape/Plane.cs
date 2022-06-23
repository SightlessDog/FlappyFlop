using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MeshFunction
{
    public Vector2 UMinMax => new Vector2(-1, 1);
    public Vector2 VMinMax => new Vector2(-1, 1);
    public Vector2Int Subdivisions => new Vector2Int(120, 120);

    public Vector3 Vertex(float u, float v) => new Vector3(
        u, v, 0);
}
