using UnityEngine;
using static Managers.Properties;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private float sendTimer = 0;
    [SerializeField] private float frequency = OBSTACLE_FREQUENCY;
    [SerializeField] private bool cheat;

    public GameObject myObstacle;
    public GameObject myBlockObstacle;
    public GameObject mainCharacter;

    private bool started;
    private float minRange;
    private float maxRange;

    void Awake()
    {
        cheat = false;
        GameManager.onGameStateChanged += StartGame;
    }

    void Destroy()
    {
        GameManager.onGameStateChanged -= StartGame;
    }

    void Update()
    {
        SetGameVar();
        if (started)
        {
            sendTimer -= Time.deltaTime;
            if (sendTimer <= 0)
            {
                CreateObstacle(0);
                CreateObstacle(DISTANCE_LEFT);
                CreateBlockObstacle(-DISTANCE_RIGHT);

                sendTimer = frequency;
            }

            if (mainCharacter != null) Time.timeScale = 1;
            else Time.timeScale = 0;
        }
    }

    public void CheatMode()
    {
        cheat = !cheat;
    }

    private void SetGameVar()
    {
        if (cheat)
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

    private void CreateBlockObstacle(float xPos)
    {
        transform.position = new Vector3(xPos, OBSTACLE_MID_RANGE, 40);
        Instantiate(myBlockObstacle, transform.position, transform.rotation);
    }

    private void CreateObstacle(float xPos)
    {
        var yPos = Random.Range(minRange, maxRange);
        transform.position = new Vector3(xPos, yPos, 40);
        Instantiate(myObstacle, transform.position, transform.rotation);
    }

    void StartGame(State state)
    {
        started = state == State.PLAY;
    }
}