using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;


public class Helice : MeshFunction
{
    public Vector2 UMinMax => new Vector2(0, 1);
    public Vector2 VMinMax => new Vector2(0, 2 * PI);
    public Vector2Int Subdivisions => new Vector2Int(120, 120);

    public Vector3 Vertex(float u, float v) => helice(u, v);
        
    public Vector3 helice(float u, float v) {
        Vector3 p;
        p.x = u * Cos(v);
        p.y = u * Sin(v);
        p.z = v;
        return p;
    }
}
