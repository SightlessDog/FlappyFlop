using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float score;
    public Text scoreText;

    void Awake()
    {
        Collision.onCollisionWithCheckpoint += increaseScore;
    }

    void Destroy ()
    {  
        Collision.onCollisionWithCheckpoint -= increaseScore;
    }

    void increaseScore()
    {
        score += score + 10;
        UpdateScoreDisplay();
    }
    
    public void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + score;
    }
}
