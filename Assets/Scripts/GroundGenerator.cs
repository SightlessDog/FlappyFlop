using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public float sendTimer = 0;
    public float frequency = 10f;
    public GameObject floor;
    private bool started;
    private bool paused;

    void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
    }
    
    void onDestroy()
    {
        GameManager.onGameStateChanged -= GameManagerOnGameStateChanged;
    }
    
    private void GameManagerOnGameStateChanged(State state)
    {
        started = state == State.PLAY;
        paused = state == State.PAUSE;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            Time.timeScale = 0;
            return;
        }
        else
        {
            Time.timeScale = 1;
        }
        if (started)
        {
            sendTimer -= Time.deltaTime;
            if (sendTimer < 0) 
            {
                Instantiate(floor, new Vector3(0, 0, -10f), transform.rotation);
                sendTimer = frequency;
            }
        }
    }
}
