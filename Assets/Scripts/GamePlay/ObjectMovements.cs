using UnityEngine;
using static Managers.Properties;

public class ObjectMovements : MonoBehaviour
{
    [SerializeField] private float life = OBJECT_LIFE_TIME;
    private bool started;
    private bool paused;
    
    void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(State state)
    {
        started = state == State.PLAY;
        paused = state == State.PAUSE;
    }

    void Update()
    {
        started = GameManager.Instance.started;
        SetPause();

        if (started)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        //Destroy object after {life}
        life -= Time.deltaTime;
        if (life <= 0) Destroy(gameObject);
        //Move object towards
        else
        {
            transform.Translate(new Vector3(0, 0, 3 * Time.deltaTime));
        }
    }

    private void SetPause()
    {
        if (paused)
        {
            Time.timeScale = 0;
            return;
        }

        Time.timeScale = 1;
    }
}