using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCinematicMovement : MonoBehaviour
{
    private bool paused;
    private void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameManagerOnGameStateChanged;
    }

    /// <summary>
    /// Start the coroutine if we are close from a checkpoint
    /// </summary>
    void LateUpdate()
    {
        RaycastHit hit;
        Vector3 origPosition = transform.localPosition;
        if (Physics.Raycast(transform.position, transform.forward,out hit, 15) && hit.collider.CompareTag("Checkpoint"))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            StartCoroutine(MoveCamera(origPosition));
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,  new Vector3(0,0, 0), 0.125f);
        }
    }
    
    /// <summary>
    /// Wait for the second frame if paused otherwise bring the camera closer 
    /// </summary>
    public IEnumerator MoveCamera(Vector3 origPosition)
    {
        while (paused)
        {
            yield return null;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition,new Vector3(origPosition.x, origPosition.y, origPosition.z + 0.04f), 0.125f) ;
        yield return null;
    }
    
    private void GameManagerOnGameStateChanged(State state)
    {
        paused = state == State.PAUSE;
    }
}
