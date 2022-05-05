using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private float sendTimer = 0;
    [SerializeField] private float frequency = 10f;
    public GameObject ground;
    public GameObject ceiling;

    // Update is called once per frame
    void Update()
    {
        sendTimer -= Time.deltaTime;
        if (sendTimer < 0) 
        {
            Instantiate(ground, new Vector3(0, -50, -10f), transform.rotation);
            Instantiate(ceiling, new Vector3(0, 100, -10f), transform.rotation);
            sendTimer = frequency;
        }
    }
}
