using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovements : MonoBehaviour
{
    public float life = 5; 
    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0) Destroy(gameObject);
        else transform.Translate(0, 0, 3*Time.deltaTime);
    }
}
