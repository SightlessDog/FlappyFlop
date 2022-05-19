using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCinematicMovement : MonoBehaviour
{
    
    void LateUpdate()
    {
        RaycastHit hit;
        Vector3 origPosition = transform.localPosition;
        if (Physics.Raycast(transform.position, transform.forward,out hit, 30) && hit.collider.CompareTag("Checkpoint"))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            StartCoroutine(MoveCamera(origPosition));
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,  new Vector3(0,0, 0), 0.125f);
        }
    }
    
    public IEnumerator MoveCamera(Vector3 origPosition)
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition,new Vector3(origPosition.x, origPosition.y, origPosition.z + 0.05f), 0.125f) ;
        yield return null;
    }
}
