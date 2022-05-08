using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelPlay;
    public GameObject panelGameOver;
    private State _state;
    private bool _isSwitchingState;
    private bool GameIsPaused;
    private GameManager Instance;
    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        SwitchState(State.MENU);
        Time.timeScale = 0;
    }
    
    public void PlayClicked()
    {
        SwitchState(State.INIT);
    }

    public void QuitClicked()
    {
        Application.Quit();
    }
    
    public void SwitchState(State newState, float delay = 1f)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }
    
    IEnumerator SwitchDelay(State newState, float delay)
    {
        _isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newState;
        BeginState(newState);
        _isSwitchingState = false;
    }
    
    void setPauseOption()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    
    public void Resume()
    {
        Cursor.visible = false;
        panelMenu.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
    }
    void Pause()
    {
        Cursor.visible = true;
        panelMenu.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0f;
    }
    
    void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                Cursor.visible = true;
                panelMenu.SetActive(true);
                break;
            case State.INIT:
                Cursor.visible = false;
                panelMenu.SetActive(false);
                SwitchState(State.PLAY);
                Time.timeScale = 1;
                break;
            case State.PLAY:
                break;
            case State.GAMEOVER:
                // We have to destroy the whole objects in the scene here 
                panelGameOver.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                setPauseOption();
                // handle switching to game over state
                break;
            case State.GAMEOVER:
                if (Input.anyKeyDown)
                {
                    SwitchState(State.MENU);
                }
                break;
        }
    }
    
    void EndState()
    {
        switch (_state)
        {
            case State.MENU:
                panelMenu.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.GAMEOVER:
                panelPlay.SetActive(false);
                panelGameOver.SetActive(false);
                break;
        }
    }
}

public enum State { MENU, INIT, PLAY, GAMEOVER }