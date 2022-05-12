using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovements : MonoBehaviour
{
    public float life = 20; 
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
    // Update is called once per frame
    void Update()
    {
        started = GameManager.Instance.started;
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
            life -= Time.deltaTime;
            if (life <= 0) Destroy(gameObject);
            else transform.Translate(0, 0, 3*Time.deltaTime);  
        }
    }
}
