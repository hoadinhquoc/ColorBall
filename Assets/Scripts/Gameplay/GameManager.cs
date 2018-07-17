using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;
	[SerializeField] GameSetting m_gameSetting;
	public GameSetting Setting{
		get{return m_gameSetting;}
	}
	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnGameStart()
	{
		GameEvents.START_GAME.Raise();
	}

	void OnGameOver()
	{

	}
}
