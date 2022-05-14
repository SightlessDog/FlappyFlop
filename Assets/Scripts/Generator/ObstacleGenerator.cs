using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private float sendTimer = 0;
    [SerializeField] private float frequency = 5f;
    [SerializeField] private bool cheat;

    public GameObject myObstacle;
    public GameObject mainCharacter;

    private bool started;
    private float minRange = 50f;
    private float maxRange = 60f;
    private float mainXPos = 0;

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
                CreateObstacle(mainXPos);
                CreateObstacle(mainXPos + 50);
                CreateObstacle(mainXPos - 50);

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
            minRange = 55f;
            maxRange = 55f;
        }
        else
        {
            minRange = 50f;
            maxRange = 60f;
        }
    }

    public void UpdateMainXPos(float xPos)
    {
        mainXPos = xPos;
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