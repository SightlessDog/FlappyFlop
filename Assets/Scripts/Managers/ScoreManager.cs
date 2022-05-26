using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float score;
    private float highScore;
    public Text scoreText;
    public Text highScoreText;

    void Awake()
    {
        Col.onCollisionWithCheckpoint += IncreaseScore;
        GameManager.onGameStateChanged += UpdateHighScore;
    }

    void OnDestroy ()
    {  
        Col.onCollisionWithCheckpoint -= IncreaseScore;
        GameManager.onGameStateChanged -= UpdateHighScore;
    }

    void IncreaseScore()
    {
        score += 10;
        UpdateScoreDisplay();
    }
    
    public void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
    }

    void UpdateHighScore(State state)
    {
        if (state == State.GAMEOVER)
        {
            highScore = PlayerPrefs.GetFloat("highScore");
            if (score > highScore)
            {
                PlayerPrefs.SetFloat("highScore", score);
            }
        }

        if (state == State.PLAY)
        {
            highScore = PlayerPrefs.GetFloat("highScore");
            highScoreText.text = "High Score: " + highScore;
        }
    }
}
