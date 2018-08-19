using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;
	[SerializeField] GameSetting m_gameSetting;
	public GameSetting Setting{
		get{return m_gameSetting;}
	}
	float m_SingleRunTimer = 0f;
	public float SingleRunTime {get{return m_SingleRunTimer;}}
	int m_score = 0;
	public int Score{get{return m_score;}}
	// Use this for initialization
	void Awake () {
		Instance = this;
		Application.targetFrameRate = 60;
		GameEvents.INSCREASE_SCORE += OnInscreaseScore;
	}

	public void OnGameStart()
	{
		m_score = 0;
		m_SingleRunTimer = 0f;
		GameEvents.SCORE_CHANGED.Raise(m_score);

		GameEvents.START_GAME.Raise();
	}

	void OnGameOver()
	{

	}

	void OnInscreaseScore(int scorePlus)
	{
		m_score += scorePlus;

		GameEvents.SCORE_CHANGED(m_score);
	}

	void LateUpdate()
	{
		m_SingleRunTimer += Time.deltaTime;
	}
}
