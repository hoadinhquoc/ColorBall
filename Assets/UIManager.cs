using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	[SerializeField] GameObject MenuMain;
	[SerializeField] GameObject MenuAP;
	[SerializeField] GameObject MenuResult;

	void Awake()
	{
		GameEvents.START_GAME += OnGameStart;
		GameEvents.GAME_OVER += OnGameOver;
	}

	void OnGameStart()
	{
		MenuMain.SetActive(false);
		MenuAP.SetActive(true);
	}

	void OnGameOver()
	{
		MenuAP.SetActive(false);
		MenuResult.SetActive(true);
	}
}
