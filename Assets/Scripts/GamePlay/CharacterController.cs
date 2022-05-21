using System.Collections;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;
using static Managers.Properties;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private bool cheat;
    [SerializeField] private float ySpeed;
    [SerializeField] private float yTarget;
    
    public int currentState;
    
    private bool started;
    private bool paused;

    void Awake()
    {
        currentState = 0;
        cheat = false;
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
        SetPause();
        BirdControl();
    }

    /// <summary>
    /// Toggle cheat mode
    /// </summary>
    public void ToggleCheatMode()
    {
        cheat = !cheat;
    }

    /// <summary>
    /// Set pause option for the game
    /// </summary>
    private void SetPause()
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
    }

    /// <summary>
    /// Control the character based on the game mode
    /// </summary>
    private void BirdControl()
    {
        if (started)
        {
            SwitchLine();
            if (cheat)
            {
                CheatBirdControl();
            }
            else
            {
                NormalBirdControl();
            }
        }
    }

    /// <summary>
    /// Ability to let character switch between lines
    /// Ability lost if the character goes out of bounds
    /// </summary>
    private void SwitchLine()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentState > -1)
        {
            gameObject.transform.Translate(-DISTANCE_LEFT, 0, 0);
            currentState--;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && currentState < 1)
        {
            gameObject.transform.Translate(DISTANCE_RIGHT, 0, 0);
            currentState++;
        }
    }

    /// <summary>
    /// Control character in normal mode
    /// Based on Flappy Bird
    /// </summary>
    private void NormalBirdControl()
    {
        gameObject.transform.Translate(0, ySpeed, 0);
        ySpeed = Mathf.Lerp(ySpeed, yTarget, 0.025f);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ySpeed = 0.25f;
        }
    }

    /// <summary>
    /// Control character in cheat mode
    /// Easily control by up and down arrow button
    /// </summary>
    private void CheatBirdControl()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gameObject.transform.Translate(0, 0.5f, 0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameObject.transform.Translate(0, -0.5f, 0);
        }
    }
}