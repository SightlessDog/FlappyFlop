using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float ySpeed;
    public float yTarget;

// Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(0, ySpeed, 0);
        ySpeed = Mathf.Lerp(ySpeed, yTarget, 0.025f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ySpeed = 0.25f;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, -90, 0));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 90, 0));
        }
    }
}