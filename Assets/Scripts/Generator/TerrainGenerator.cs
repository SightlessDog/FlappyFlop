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
    [SerializeField] private DirectionType currentDirection = DirectionType.FORWARD;

    public GameObject ground;
    public GameObject ceiling;

    [SerializeField] private ObjectMovements groundObjectMovements;
    [SerializeField] private ObjectMovements ceilingObjectMovements;

    private bool started;

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
                GenerateRandomCeilingAndGround();
                sendTimer = frequency;
            }
        }
    }

    private void GenerateCeilingAndGround(Quaternion quaternion)
    {
        var groundObj = Instantiate(ground, new Vector3(0, -50, 0), transform.rotation * quaternion);
        var ceilingObj = Instantiate(ceiling, new Vector3(0, 100, -0), transform.rotation * quaternion);
        groundObjectMovements = groundObj.GetComponent<ObjectMovements>();
        ceilingObjectMovements = ceilingObj.GetComponent<ObjectMovements>();
    }

    private void GenerateRandomCeilingAndGround()
    {
        var nextDir = GetNextRandomDirection(currentDirection);
        if (nextDir == DirectionType.FORWARD || nextDir == DirectionType.BACKWARD)
        {
            GenerateCeilingAndGround(Quaternion.Euler(0, 0, 0));
        }
        else
        {
            GenerateCeilingAndGround(Quaternion.Euler(0, 90, 0));
        }

        groundObjectMovements.directionType = nextDir;
        ceilingObjectMovements.directionType = nextDir;
        currentDirection = nextDir;
    }

    private bool RandomDir()
    {
        var rand = new Random();
        var result = rand.Next(2);
        return result == 1;
    }

    private DirectionType GetNextRandomDirection(DirectionType curDir)
    {
        if (curDir == DirectionType.FORWARD || curDir == DirectionType.BACKWARD)
        {
            return RandomDir() ? DirectionType.LEFT : DirectionType.RIGHT;
        }

        return RandomDir() ? DirectionType.BACKWARD : DirectionType.FORWARD;
    }

    void StartGame(State state)
    {
        started = state == State.PLAY;
    }
}