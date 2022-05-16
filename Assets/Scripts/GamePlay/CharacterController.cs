using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Managers.Properties;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private bool cheat;

    public float ySpeed;
    public float yTarget;

    private bool started;
    private bool paused;

    void Awake()
    {
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
        Pause();
        BirdControl();
    }

    public void CheatMode()
    {
        cheat = !cheat;
    }

    private void Pause()
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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(-DISTANCE_LEFT, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(DISTANCE_RIGHT, 0, 0);
        }
    }

    private void NormalBirdControl()
    {
        gameObject.transform.Translate(0, ySpeed, 0);
        ySpeed = Mathf.Lerp(ySpeed, yTarget, 0.025f);

        if (Input.GetKeyDown(KeyCode.Space))
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
}