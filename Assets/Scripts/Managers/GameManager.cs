using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static State;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public State state;
    private bool paused;
    public float gameSpeed = 1;
    public bool started;
    public static event Action<State> onGameStateChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(INIT);
    }

    public void PlayClicked()
    {
        started = true;
        UpdateGameState(PLAY);
    }

    public void QuitClicked()
    {
        Application.Quit();
    }

    public void UpdateGameState(State state)
    {
        this.state = state;

        switch (state)
        {
            case INIT:
                break;
            case MENU:
                break;
            case PLAY:
                break;
            case PAUSE:
                break;
            case GAMEOVER:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        onGameStateChanged?.Invoke(state);
    }
    
    void Update()
    {
        PauseManager();
        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = gameSpeed;
        }
    }

    private void PauseManager()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && state != GAMEOVER)
        {
            paused = !paused;
            UpdateGameState(paused ? PAUSE : PLAY);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && state == GAMEOVER)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void SetGameSpeed(float speed)
    {
        gameSpeed = speed;
    }
}

public enum State
{
    MENU,
    INIT,
    PLAY,
    PAUSE,
    GAMEOVER
}