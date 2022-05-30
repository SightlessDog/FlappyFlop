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
        jumpboostMode = false;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    /// <summary>
    /// Actions when the character goes through the assigned checkout
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Checkpoint"))
        {
            // Disable increasing jumping boost in cheat mode
            if (jumpboostMode) checkpoints++;
            // If the previous coroutine still running stop it
            StopAllCoroutines();
            // Shake the camera
            StartCoroutine(cameraShake.Shake(.03f, .3f));
            //Play the audio
            audioSource.PlayOneShot(checkpointPassed);
            onCollisionWithCheckpoint?.Invoke();
        }
    }

    /// <summary>
    /// Destroy the object if it hits the obstacles/objects
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Env"))
        {
            Destroy(gameObject);
            GameManager.Instance.UpdateGameState(State.GAMEOVER);
        }
    }

    /// <summary>
    /// Toggle Jump Boost mode
    /// </summary>
    public void ToggleJumpBoostMode()
    {
        jumpboostMode = !jumpboostMode;
    }
}