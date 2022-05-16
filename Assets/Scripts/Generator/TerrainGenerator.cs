using UnityEngine;
using static Managers.Properties;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private float sendTimer = 0;
    [SerializeField] private float frequency = TERRAIN_FREQUENCY;

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
                GenerateCeilingAndGround(0);
                GenerateCeilingAndGround(DISTANCE_LEFT);
                GenerateCeilingAndGround(-DISTANCE_RIGHT);
                sendTimer = frequency;
            }
        }
    }

    private void GenerateCeilingAndGround(float xPos)
    {
        Instantiate(ground, new Vector3(xPos, -50, 0), transform.rotation);
        Instantiate(ceiling, new Vector3(xPos, 100, 0), transform.rotation);
    }

    void StartGame(State state)
    {
        started = state == State.PLAY;
    }
}