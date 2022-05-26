using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameOverPanel;

    // Subscribe the event on awake
    void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
    }

    // Unsubscribe to avoid memory leaks
    void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(State state)
    {
        menuPanel.SetActive(state == State.INIT || state == State.PAUSE);
        gameOverPanel.SetActive(state == State.GAMEOVER);
    }
}
