using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameOverPanel;

    void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
    }

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
