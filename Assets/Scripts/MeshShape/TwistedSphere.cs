using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class TwistedSphere : MeshFunction
{
    public Vector2 UMinMax => new Vector2(-1, 1);
    public Vector2 VMinMax => new Vector2(-1, 1);
    public Vector2Int Subdivisions => new Vector2Int(120, 120);

    public Vector3 Vertex(float u, float v) =>  Sphere(u, v);
        
    /// <summary>
    /// Formula of a twisted sphere
    /// </summary>
    public Vector3 Sphere (float u, float v) {
        float r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v));
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(0.5f * PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }
}
