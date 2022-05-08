using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float ySpeed;
    public float yTarget;
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
            gameObject.transform.Translate(0, ySpeed, 0);
            ySpeed = Mathf.Lerp(ySpeed, yTarget, 0.025f);

            if (Input.GetKeyDown("space"))
            {
                ySpeed = 0.25f;
            }   
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, -90, 0));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 90, 0));
        }
    }
}