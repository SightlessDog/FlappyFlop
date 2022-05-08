using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;

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
        _menuPanel.SetActive(state == State.INIT || state == State.PAUSE);
    }
}
