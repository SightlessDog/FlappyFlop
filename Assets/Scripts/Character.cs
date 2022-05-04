using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float ySpeed;
    public float yTarget;

// Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(0, ySpeed, 0);
        ySpeed = Mathf.Lerp(ySpeed, yTarget, 0.025f);

        if (Input.GetKeyDown("space"))
        {
            ySpeed = 0.25f;
        }
    }
}