using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Col : MonoBehaviour
{
    [SerializeField] private bool jumpboostMode;

    public static event Action onCollisionWithCheckpoint;
    public AudioClip checkpointPassed; 
    AudioSource audioSource;
    public CamerShake cameraShake;
    public int checkpoints = 0;

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Checkpoint"))
        {
            // Disable increasing jumping boost in cheat mode
            if(!jumpboostMode) checkpoints++;
            // If the previous coroutine still running stop it
            StopAllCoroutines();
            // Destroy Checkpoint
            Destroy(collision.collider);
            // Shake the camera
            StartCoroutine(cameraShake.Shake(.03f, .3f));
            audioSource.PlayOneShot(checkpointPassed);
            onCollisionWithCheckpoint?.Invoke();
        }
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Env") || collider.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            GameManager.Instance.UpdateGameState(State.GAMEOVER);
        }
    }

    /// <summary>
    /// Toggle Cheat mode
    /// </summary>
    public void ToggleJumpBoostMode()
    {
        jumpboostMode = !jumpboostMode;
    }
}
