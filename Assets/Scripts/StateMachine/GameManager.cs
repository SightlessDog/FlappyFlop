using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public State state;
  private bool paused = false;
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
    Debug.Log("hello");
    if (Input.GetKeyDown("escape"))
    {
      paused = !paused;
      UpdateGameState(paused ? State.PAUSE : State.PLAY);
    }
  }
}

public enum State { MENU, INIT, PLAY, PAUSE, GAMEOVER }