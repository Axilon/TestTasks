using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {
    
    private int currentScore;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text endGameScoreText;
    [SerializeField]
    private Text healthText;
    public static ScoreManager instance;
	// Use this for initialization
	void Awake () {
		if(instance== null)
        {
            instance = this;
        }
        scoreText.enabled = false;
        healthText.enabled = false;
    }
	public void InitializeScore()
    {
        currentScore = 0;
        UpdateScoreText();
        scoreText.enabled = true;
        healthText.enabled = true;
    }
    public void AddScore(int score)
    {
        currentScore += score;
        UpdateScoreText();
    }
    public void SetEndGameScore()
    {
        scoreText.enabled = false;
        healthText.enabled = false;
        endGameScoreText.text = "Your score: " + currentScore.ToString();
    }
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }
    public void UpdateHealth(int health)
    {
        healthText.text = "Health: " + health.ToString();
    }
}
