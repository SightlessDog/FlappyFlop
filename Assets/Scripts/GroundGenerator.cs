using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public float sendTimer = 0;
    public float frequency = 10f;
    public GameObject floor;

    // Update is called once per frame
    void Update()
    {
        sendTimer -= Time.deltaTime;
        if (sendTimer < 0) 
        {
            Instantiate(floor, new Vector3(0, 0, -10f), transform.rotation);
            sendTimer = frequency;
        }
    }
}
