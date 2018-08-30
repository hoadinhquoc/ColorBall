using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	MoveToPoint moveComponent;
    PerfectStar perfectStar;
	// Use this for initialization
	void Awake () {
		moveComponent = GetComponent<MoveToPoint>();
        perfectStar = GetComponentInChildren<PerfectStar>();

        perfectStar.gameObject.SetActive(false);
        gameObject.SetActive(false);
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
    public void UpdateStarColor(Color color)
    {
        perfectStar.UpdateColor(color);
    }
    public void ShowPerfectStar()
    {
        perfectStar.gameObject.SetActive(true);
    }
	public void Run()
	{
		if(Time.frameCount%2 == 0)
		{
			Vector3 temp = moveComponent.StartPoint;
			moveComponent.StartPoint = moveComponent.EndPoint;
			moveComponent.EndPoint = temp;
		}
		moveComponent.enabled = true;
		moveComponent.SetEndCallback(()=>{ObstacleSpawner.Instance.OnChildRemoved(this);});
		gameObject.SetActive(true);
	}
}
