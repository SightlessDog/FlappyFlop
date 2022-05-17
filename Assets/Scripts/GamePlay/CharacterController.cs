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

    public void ToggleCheatMode()
    {
        cheat = !cheat;
    }

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

    private void SwitchLine()
    {
        if (IsCharAlive() && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(-DISTANCE_LEFT, 0, 0);
            currentState--;
        }

        if (IsCharAlive() && Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(DISTANCE_RIGHT, 0, 0);
            currentState++;
        }
    }

    private void NormalBirdControl()
    {
        gameObject.transform.Translate(0, ySpeed, 0);
        ySpeed = Mathf.Lerp(ySpeed, yTarget, 0.025f);
        
        if (IsCharAlive() && Input.GetKeyDown(KeyCode.Space))
        {
            ySpeed = 0.25f;
        }
    }

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

    private bool IsCharAlive()
    {
        if (currentState > 1 || currentState < -1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}