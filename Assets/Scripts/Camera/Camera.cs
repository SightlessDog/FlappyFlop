using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using GamePlay;
using UnityEngine;
using static GamePlay.CharacterState;

public class Camera : MonoBehaviour
{
    public GameObject mainCharacter;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed = 30.0f;
    [SerializeField] private float zRot = 0.0f;
    [SerializeField] private bool hardMode;

    private bool started;
    private bool paused;

    private void Awake()
    {
        hardMode = false;
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
    }

    // To make sure that the camera movement is gonna happen last
    void LateUpdate()
    {
        if (mainCharacter != null)
        {
            if (hardMode && started && !paused)
            {
                SetZRotation();
            }
            SetCamPosition();
        }
    }

    // Make the camera follow the bird
    private void SetCamPosition()
    {
        Vector3 desiredPosition = mainCharacter.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(mainCharacter.transform);
        transform.Rotate(0, 0, zRot);
    }

    private void SetZRotation()
    {
        if (zRot > 360f)
        {
            zRot = 0f;
        }

        float delta = Time.deltaTime * speed;
        zRot += delta;
    }

    public void ToggleHardMode()
    {
        hardMode = !hardMode;
    }
    
    private void GameManagerOnGameStateChanged(State state)
    {
        started = state == State.PLAY;
        paused = state == State.PAUSE;
    }
}