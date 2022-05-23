using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public State state;
  private bool paused;
  public bool started;
  public static event Action<State> onGameStateChanged;

  void Awake()
  {
    Instance = this;
  }

  void Start()
  {
    UpdateGameState(State.INIT);
  }

  public void PlayClicked()
  {
    Debug.Log("PlayClicked");
    started = true;
    UpdateGameState(State.PLAY);
  }

  public void QuitClicked()
  {
    Debug.Log("QuitClicked");
    Application.Quit();
  }

  public void UpdateGameState(State state)
  {
    this.state = state;

    switch (state)
    {
      case State.INIT:
        break;
      case State.MENU:
        break;
      case State.PLAY:
        break;
      case State.PAUSE:
        break;
      case State.GAMEOVER:
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(state), state, null);
    }

    onGameStateChanged?.Invoke(state);
  }

  private void Update()
  {
    if (Input.GetKeyDown("escape") && state != State.GAMEOVER)
    {
      paused = !paused;
      UpdateGameState(paused ? State.PAUSE : State.PLAY);
    }
    else if (Input.GetKeyDown("escape") && state == State.GAMEOVER)
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  }
}

public enum State { MENU, INIT, PLAY, PAUSE, GAMEOVER }