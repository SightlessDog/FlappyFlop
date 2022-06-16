
    using UnityEngine;
    using static UnityEngine.Mathf;

    public class TwistedTorus : MeshFunction
    {
        public Vector2 UMinMax => new Vector2(-1, 1);
        public Vector2 VMinMax => new Vector2(-1, 1);
        public Vector2Int Subdivisions => new Vector2Int(100, 100);

        public Vector3 Vertex(float u, float v) => new Vector3(
            u, v, 0);
        
        public Vector3 Torus (float u, float v) {
            float r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f));
            float r2 = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f));
            float s = r1 + r2 * Cos(PI * v);
            Vector3 p;
            p.x = s * Sin(PI * u);
            p.y = r2 * Sin(PI * v);
            p.z = s * Cos(PI * u);
            return p;
        }
    }
