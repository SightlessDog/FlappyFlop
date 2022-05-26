using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Col : MonoBehaviour
{
    public static event Action onCollisionWithCheckpoint;
    public AudioClip checkpointPassed; 
    AudioSource audioSource;
    public CamerShake cameraShake;

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Checkpoint"))
        {
            Debug.Log("Passed through a checkpoint");
            StartCoroutine(cameraShake.Shake(.02f, .2f));
            audioSource.PlayOneShot(checkpointPassed);
            onCollisionWithCheckpoint?.Invoke();
        }
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Env"))
        {
            Destroy(gameObject);
            Debug.Log("Collision and object destroyed");
            GameManager.Instance.UpdateGameState(State.GAMEOVER);
        }
    }
}
