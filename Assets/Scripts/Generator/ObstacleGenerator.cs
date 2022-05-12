using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private float sendTimer = 0;
    [SerializeField] private float frequency = 5f;
    public GameObject myObstacle;
    public GameObject mainCharacter;
    private bool started;

    void Awake()
    {
        GameManager.onGameStateChanged += StartGame;
    }

    void Destroy()
    {
        GameManager.onGameStateChanged -= StartGame;
    }
    
    void Update()
    {
        if (started)
        {
            sendTimer -= Time.deltaTime;
            if (sendTimer <= 0)
            {
                createObstacle();
                sendTimer = frequency;
            }
            if (mainCharacter != null) Time.timeScale = 1;
            else Time.timeScale = 0;    
        }
    }

    private void createObstacle()
    {
        var yPos = Random.Range(50f, 60f);
        transform.position = new Vector3(0, yPos, 40);
        Instantiate(myObstacle, transform.position, transform.rotation);
    }

    void StartGame(State state)
    {
        started = state == State.PLAY;
    }
}
