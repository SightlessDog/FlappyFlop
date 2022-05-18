using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCinematicMovement : MonoBehaviour
{
    void LateUpdate()
    {
        RaycastHit hit;
        Quaternion origRot = transform.rotation;
        if (Physics.Raycast(transform.position, transform.forward,out hit, 10))
        {
            Debug.Log("From the camera there is something " + hit.collider.tag);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            // transform.rotation = Quaternion.Euler(new Vector3(0, 1, 0));
        }
        else
        {
            // transform.rotation = Quaternion.Euler(0, 180, 0);
            Debug.Log("there is nothing");
        }
    }
}
