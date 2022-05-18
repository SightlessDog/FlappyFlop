using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public static event Action onCollisionWithCheckpoint;
    public AudioClip checkpointPassed; 
    AudioSource audioSource;
    public CamerShake cameraShake;

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, 25))
        {
               
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Env")
        {
            Destroy(gameObject);
            Debug.Log("Collision and object destroyed");
            GameManager.Instance.UpdateGameState(State.GAMEOVER);
        } 
        else if (other.gameObject.tag == "Checkpoint")
        {
            Debug.Log("Passed through a checkpoint");
            StartCoroutine(cameraShake.Shake(.06f, .6f));
            audioSource.PlayOneShot(checkpointPassed);
            onCollisionWithCheckpoint?.Invoke();
        }
    }
}
