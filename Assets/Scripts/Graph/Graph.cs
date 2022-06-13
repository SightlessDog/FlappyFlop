using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab;
    [SerializeField, Range(10, 100)] private int resolution = 10;
    Transform[] points;
    [SerializeField] FunctionLibrary.FunctionName function;
    
    void Awake()
    {
        points = new Transform[resolution * resolution];
        float step = 2f / resolution;
        var scale = Vector3.one * step;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i] = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
        }
    }

    void Update()
    {
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        float time = Time.time;
        float step = 2f / resolution;
        float v = 0.5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++) {
            if (x == resolution) {
                x = 0;
                z += 1;
                v = (z + 0.5f) * step - 1f;
            }
            float u = (x + 0.5f) * step - 1f;
            points[i].localPosition = f(u, v, time);
        }
    }
}
