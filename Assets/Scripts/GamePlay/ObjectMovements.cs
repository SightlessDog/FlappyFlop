using System;
using Generator;
using UnityEngine;
using Random = System.Random;

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

    void Update()
    {
        started = GameManager.Instance.started;
        Pause();

        if (started)
        {
            life -= Time.deltaTime;
            if (life <= 0) Destroy(gameObject);
            else
            {
                transform.Translate(new Vector3(0, 0, 3 * Time.deltaTime));
            }
        }
    }

    private void Pause()
    {
        if (paused)
        {
            Time.timeScale = 0;
            return;
        }

        Time.timeScale = 1;
    }

    // private Vector3 SwitchTranslate()
    // {
    //     speed = 3 * Time.deltaTime;
    //     
    //     return directionType switch
    //     {
    //         DirectionType.RIGHT => new Vector3(-speed, 0, 0),
    //         DirectionType.LEFT => new Vector3(speed, 0, 0),
    //         DirectionType.BACKWARD => new Vector3(0, 0, -speed),
    //         DirectionType.FORWARD => new Vector3(0, 0, speed),
    //         _ => throw new ArgumentOutOfRangeException()
    //     };
    // }
}