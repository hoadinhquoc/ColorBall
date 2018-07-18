using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	MoveToPoint moveComponent;
	// Use this for initialization
	void Awake () {
		moveComponent = GetComponent<MoveToPoint>();

	}
	
	void OnEnable()
	{
		moveComponent.enabled = true;
		moveComponent.ResetTimer();

		GameEvents.GAME_OVER += OnGameOver;
	}

	void OnDisable()
	{
		GameEvents.GAME_OVER -= OnGameOver;
	}
	void OnGameOver()
	{
		moveComponent.enabled = false;
	}
	public void SetPositionY(float y)
	{
		moveComponent.StartPoint.y = y;
		moveComponent.EndPoint.y = y;
	}
	public void SetSpeed(float speed)
	{
		moveComponent.Speed = speed;
	}

	public void Run()
	{
		moveComponent.enabled = true;
		moveComponent.SetEndCallback(()=>{ObstacleSpawner.Instance.OnChildRemoved(this);});
	}
}
