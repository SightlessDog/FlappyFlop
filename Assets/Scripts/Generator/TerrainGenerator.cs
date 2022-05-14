using System;
using System.Collections;
using System.Collections.Generic;
using Generator;
using UnityEngine;
using Random = System.Random;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private float sendTimer = 0;
    [SerializeField] private float frequency = 10f;

    public GameObject ground;
    public GameObject ceiling;

    private bool started;
    private float mainXPos = 0;

    void Awake()
    {
        GameManager.onGameStateChanged += StartGame;
    }

    void Update()
    {
        if (started)
        {
            sendTimer -= Time.deltaTime;
            if (sendTimer < 0)
            {
                GenerateCeilingAndGround(mainXPos);
                GenerateCeilingAndGround(mainXPos+50);
                GenerateCeilingAndGround(mainXPos-50);
                sendTimer = frequency;
            }
        }
    }

    private void GenerateCeilingAndGround(float xPos)
    {
        Instantiate(ground, new Vector3(xPos, -50, 0), transform.rotation);
        Instantiate(ceiling, new Vector3(xPos, 100, 0), transform.rotation);
    }
    
    public void UpdateMainXPos(float xPos)
    {
        mainXPos = xPos;
    }
    
    void StartGame(State state)
    {
        started = state == State.PLAY;
    }
}