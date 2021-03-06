using System;
using GamePlay;
using UnityEngine;
using static GamePlay.CharacterState;
using static Managers.Properties;
using Random = UnityEngine.Random;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private float sendTimer = 0;
    [SerializeField] private float frequency;
    [SerializeField] private int sendBlockTimer = BLOCK_OBSTACLE_FREQUENCY;
    [SerializeField] private bool staticObstaclesMode;
    [SerializeField] private GameObject parent;
    [SerializeField] private CharacterState currentCharacterState;

    public GameObject myObstacle;
    public GameObject myBlockObstacle;
    public GameObject mainCharacter;

    private bool started;
    private float minRange;
    private float maxRange;

    void Awake()
    {
        staticObstaclesMode = false;
        frequency = OBSTACLE_FREQUENCY;
        parent = GameObject.FindGameObjectWithTag("AutoGenerated");
        UpdateCurrentCharacterState();
        GameManager.onGameStateChanged += StartGame;
    }

    void OnDestroy()
    {
        GameManager.onGameStateChanged -= StartGame;
    }

    void StartGame(State state)
    {
        started = state == State.PLAY;
    }

    void Update()
    {
        SetGameVar();
        UpdateCurrentCharacterState();
        if (started)
        {
            SendObstacles();
        }
    }

    /// <summary>
    /// Send Obstacles to the game play
    /// </summary>
    private void SendObstacles()
    {
        sendTimer -= Time.deltaTime;
        //obstacles will be sent when the timer reach 0, then reset the timer = frequency
        if (sendTimer <= 0)
        {
            GenerateObstacles();
            sendTimer = frequency;
        }
    }

    /// <summary>
    /// Generate Obstacles on all 3 lines
    /// </summary>
    private void GenerateObstacles()
    {
        //A block obstacle will be sent when the timer reached 0 to force player to switch to another line
        sendBlockTimer--;
        if (sendBlockTimer == 0)
        {
            switch (currentCharacterState)
            {
                case MIDDLE:
                    CreateBlockObstacle(0);
                    CreateObstacle(DISTANCE_LEFT);
                    CreateObstacle(-DISTANCE_RIGHT);
                    break;
                case LEFT:
                    CreateObstacle(0);
                    CreateBlockObstacle(DISTANCE_LEFT);
                    CreateObstacle(-DISTANCE_RIGHT);
                    break;
                case RIGHT:
                    CreateObstacle(0);
                    CreateObstacle(DISTANCE_LEFT);
                    CreateBlockObstacle(-DISTANCE_RIGHT);
                    break;
                case DEAD:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            sendBlockTimer = BLOCK_OBSTACLE_FREQUENCY;
        }
        else
        {
            CreateObstacle(0);
            CreateObstacle(DISTANCE_LEFT);
            CreateObstacle(-DISTANCE_RIGHT);
        }
    }

    /// <summary>
    /// Toggle Cheat mode
    /// </summary>
    public void ToggleStaticObstaclesMode()
    {
        staticObstaclesMode = !staticObstaclesMode;
    }

    /// <summary>
    /// Set the y position variable for the obstacles
    /// Cheat mode will keep the obstacle static
    /// </summary>
    private void SetGameVar()
    {
        if (staticObstaclesMode)
        {
            minRange = OBSTACLE_MID_RANGE;
            maxRange = OBSTACLE_MID_RANGE;
        }
        else
        {
            minRange = OBSTACLE_MIN_RANGE;
            maxRange = OBSTACLE_MAX_RANGE;
        }
    }

    /// <summary>
    /// Create a block obstacle based on the given xPos
    /// </summary>
    /// <param name="xPos"></param>
    private void CreateBlockObstacle(float xPos)
    {
        transform.position = new Vector3(xPos, OBSTACLE_MID_RANGE, 40);
        Instantiate(myBlockObstacle, transform.position, transform.rotation, parent.transform);
    }

    /// <summary>
    /// Create obstacles on ground and ceiling based on the given xPos
    /// </summary>
    /// <param name="xPos"></param>
    private void CreateObstacle(float xPos)
    {
        //yPos will be random generated between minRange & maxRange
        var yPos = Random.Range(minRange, maxRange);
        transform.position = new Vector3(xPos, yPos, 40);
        Instantiate(myObstacle, transform.position, transform.rotation, parent.transform);
    }

    /// <summary>
    /// Randomly decide to create either normal obstacle or a block obstacle
    /// </summary>
    /// <param name="xPos"></param>
    private void CreateRandomObstacle(float xPos)
    {
        if (RandomBool())
        {
            CreateObstacle(xPos);
        }
        else
        {
            CreateBlockObstacle(xPos);
        }
    }

    /// <summary>
    /// generate a random bool
    /// </summary>
    /// <returns></returns>
    private bool RandomBool()
    {
        return Random.value > 0.5f;
    }

    /// <summary>
    /// Update current State of the character
    /// </summary>
    private void UpdateCurrentCharacterState()
    {
        if (mainCharacter != null)
        {
            currentCharacterState = mainCharacter.GetComponent<CharacterController>().currentState;
        }
    }
}