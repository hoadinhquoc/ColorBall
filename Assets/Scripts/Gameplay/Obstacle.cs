using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    [SerializeField] PerfectStar LeftPerfect;
    [SerializeField] PerfectStar RightPerfect;
    MoveToPoint moveComponent;
    bool m_needShowPerfect = false;
    
	// Use this for initialization
	void Awake () {
		moveComponent = GetComponent<MoveToPoint>();

        LeftPerfect.gameObject.SetActive(false);
        RightPerfect.gameObject.SetActive(false);
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
        LeftPerfect.UpdateColor(color);
        RightPerfect.UpdateColor(color);
    }
    public void ShowPerfectStar()
    {
        m_needShowPerfect = true;
    }
	public void Run()
	{
		if(Time.frameCount%2 == 0)
		{
			Vector3 temp = moveComponent.StartPoint;
			moveComponent.StartPoint = moveComponent.EndPoint;
			moveComponent.EndPoint = temp;
		}

        if ((moveComponent.StartPoint.x - moveComponent.EndPoint.y) > 0) //right to left
            RightPerfect.gameObject.SetActive(true);
        else
            LeftPerfect.gameObject.SetActive(true);

        moveComponent.enabled = true;
		moveComponent.SetEndCallback(()=>{ObstacleSpawner.Instance.OnChildRemoved(this);});
		gameObject.SetActive(true);
	}
}
