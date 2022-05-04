using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float sendTimer = 1;
    public float frequency = 2;
    public float position;
    public GameObject myObstacle;
    public GameObject mainCharacter;

    // Update is called once per frame
    void Update()
    {
        sendTimer -= Time.deltaTime;
        if (sendTimer <= 0)
        {
            position = Random.Range(50f, 70f);
            transform.position = new Vector3(-83.8f, position, 70f);
            Instantiate(myObstacle, transform.position, transform.rotation);
            sendTimer = frequency;
        }
        
        if (mainCharacter != null) Time.timeScale = 1;
        else Time.timeScale = 0;
    }
}
