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

    /// <summary>
    /// Initialize the game
    /// </summary>
    void Start()
    {
        UpdateGameState(INIT);
    }

    /// <summary>
    /// Go to play mode
    /// </summary>
    public void PlayClicked()
    {
        started = true;
        UpdateGameState(PLAY);
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitClicked()
    {
        Application.Quit();
    }

    /// <summary>
    /// Update game state
    /// </summary>
    /// <param name="state"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
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

    /// <summary>
    /// Pause Option
    /// </summary>
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

    /// <summary>
    /// Adjust the game speed
    /// </summary>
    /// <param name="speed"></param>
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