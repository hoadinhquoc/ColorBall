using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPhase : MonoBehaviour {

	void Awake()
	{
		GameEvents.START_GAME += OnGameStart;
	}
	public void OnChangeDirectionTap()
	{
		GameEvents.MC_CHANGE_DIRECTION.Raise();
	}

	void OnGameStart()
	{
		this.gameObject.SetActive(true);
	}
}
