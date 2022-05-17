using UnityEngine;
using static Managers.Properties;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private float sendTimer = 0;
    [SerializeField] private float frequency = TERRAIN_FREQUENCY;
    [SerializeField] private GameObject parent;

    public GameObject ground;
    public GameObject ceiling;

    private bool started;

    void Awake()
    {
        parent = GameObject.FindGameObjectWithTag("AutoGenerated");
        GameManager.onGameStateChanged += StartGame;
    }

    void Update()
    {
        if (started)
        {
           SendTerrain();
        }
    }

    /// <summary>
    /// Generate terrain on 3 lines
    /// </summary>
    private void SendTerrain()
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

    /// <summary>
    /// Generate both ground and ceiling for the game
    /// </summary>
    /// <param name="xPos"></param>
    private void GenerateCeilingAndGround(float xPos)
    {
        Instantiate(ground, new Vector3(xPos, -50, 0), transform.rotation, parent.transform);
        Instantiate(ceiling, new Vector3(xPos, 100, 0), transform.rotation, parent.transform);
    }

    void StartGame(State state)
    {
        started = state == State.PLAY;
    }
}