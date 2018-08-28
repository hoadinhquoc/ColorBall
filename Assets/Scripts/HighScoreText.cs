using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreText : MonoBehaviour {
    TextMeshProUGUI highScoreText;
    int m_highScore = 0;
	// Use this for initialization
	void Awake () {
        highScoreText = GetComponent<TextMeshProUGUI>();
        m_highScore = PlayerPrefs.GetInt("HighScore", 0);
        GameEvents.SCORE_CHANGED += UpdateHighScore;
    }
	

    void UpdateHighScore(int score)
    {
        if(score > m_highScore)
        {
            m_highScore = score;
           
        }
    }
    private void OnEnable()
    {
        highScoreText.text = "Best: <#FF005B>" + m_highScore.ToString();
    }
    private void OnDisable()
    {
        PlayerPrefs.SetInt("HighScore", m_highScore);
    }
}
