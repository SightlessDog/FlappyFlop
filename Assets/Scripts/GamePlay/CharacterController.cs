using System.Collections;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;
using static GamePlay.CharacterState;
using static Managers.Properties;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private bool cheat;
    [SerializeField] private float ySpeed;
    [SerializeField] private float yTarget;
    [SerializeField] private float yBoost = 0f;

    public CharacterState currentState;
    
    private int currentStateIndex;
    private int checkpoints;
    private bool started;

    // Subscribe the event on awake
    void Awake()
    {
        currentStateIndex = 0;
        cheat = false;
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
    }

    // Unsubscribe the event
    void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameManagerOnGameStateChanged;
    }

    // The event handler
    private void GameManagerOnGameStateChanged(State state)
    {
        started = state == State.PLAY;
    }

    // Update is called once per frame
    void Update()
    {
        BirdControl();
        currentState = GetCurrentCharacterState();
    }

    /// <summary>
    /// Toggle cheat mode
    /// </summary>
    public void ToggleCheatMode()
    {
        cheat = !cheat;
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
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentStateIndex > -1)
        {
            gameObject.transform.Translate(-DISTANCE_LEFT, 0, 0);
            currentStateIndex--;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && currentStateIndex < 1)
        {
            gameObject.transform.Translate(DISTANCE_RIGHT, 0, 0);
            currentStateIndex++;
        }
    }

    /// <summary>
    /// Control character in normal mode
    /// Based on Flappy Bird
    /// </summary>
    private void NormalBirdControl()
    {
        ChangeSpeedAccordingToCheckpoints();
        gameObject.transform.Translate(0, ySpeed, 0);
        ySpeed = Mathf.Lerp(ySpeed, yTarget, 0.025f);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ySpeed = 0.25f + yBoost;
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
            gameObject.transform.Translate(0, 1f, 0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameObject.transform.Translate(0, -1f, 0);
        }
    }
    
    /// <summary>
    /// Return current state of the character
    /// </summary>
    private CharacterState GetCurrentCharacterState()
    {
        return currentStateIndex switch
            {
                0 => MIDDLE,
                1 => RIGHT,
                -1 => LEFT,
                _ => DEAD
            };
    }

    /// <summary>
    /// Increase the jumping boost as the player progresses 
    /// </summary>
    void ChangeSpeedAccordingToCheckpoints()
    {
        checkpoints = GetComponent<Col>().checkpoints;
        if (checkpoints < 2) yBoost = 0f;
        else if (checkpoints < 4) yBoost = 0.05f;
        else if (checkpoints < 6) yBoost = 0.1f;
        else if (checkpoints < 8) yBoost = 0.15f;
        else yBoost = 0.2f;

    }
}