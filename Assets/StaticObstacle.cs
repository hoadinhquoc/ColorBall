using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacle : MonoBehaviour {
	enum State{
		HIDE,
		SHOW_INDICATOR,
		MOVING_IN,
		STAND,
		MOVING_OUT
	}
	[SerializeField] Transform TopLaser;
	[SerializeField] GameObject TopCollider;
	[SerializeField] Transform BottomLaser;
	[SerializeField] GameObject BottomCollider; 
	[SerializeField] float TimeToShowIndicator = 1f;
	[SerializeField] float TimeStandBy = 2.5f;
	[SerializeField] float Speed = 4f;
	[SerializeField] float TopStartPositionY = 7f;
	[SerializeField] float TopDestinationPositionY = 1f;
	State m_state = State.HIDE;
	float m_IndicatorTimer = 0f;
	float m_StandTimer = 0f;
	
	// Use this for initialization
	void Awake()
	{
		GameEvents.RUN_STATIC_OBSTACLE += Run;
		GameEvents.GAME_OVER += Reset;
		Reset();
	}
	void Reset()
	{
		TopLaser.gameObject.SetActive(false);
		BottomLaser.gameObject.SetActive(false);
		m_state = State.HIDE;
		m_StandTimer = 0f;
		m_IndicatorTimer = 0f;
	}
	void Run()
	{
		m_state = State.SHOW_INDICATOR;
		TopLaser.transform.position = new Vector3(0f,TopStartPositionY, 0f);
		BottomLaser.transform.position = new Vector3(0f, -TopStartPositionY, 0f); 
		TopCollider.SetActive(false);
		BottomCollider.SetActive(false);
		TopLaser.gameObject.SetActive(true);
		BottomLaser.gameObject.SetActive(true);
	}
	// Update is called once per frame
	void Update () {
		float dt = Time.deltaTime;
		if(m_state == State.MOVING_IN)
		{
			Vector3 topLaserPos = TopLaser.position;
			topLaserPos.y -= Speed * dt;
			TopLaser.position = topLaserPos;

			Vector3 bottomLaserPos = BottomLaser.position;
			bottomLaserPos.y += Speed * dt;
			BottomLaser.position = bottomLaserPos; 

			if(topLaserPos.y < TopDestinationPositionY)
				m_state = State.STAND;
		}
		else if(m_state == State.MOVING_OUT)
		{
			Vector3 topLaserPos = TopLaser.position;
			topLaserPos.y += Speed * dt;
			TopLaser.position = topLaserPos;

			Vector3 bottomLaserPos = BottomLaser.position;
			bottomLaserPos.y -= Speed * dt;
			BottomLaser.position = bottomLaserPos;

			if(topLaserPos.y > TopStartPositionY)
			{
				Reset();
				GameEvents.END_STATIC_OBSTACLE.Raise();
			}
		}
		else if(m_state == State.SHOW_INDICATOR)
		{
			m_IndicatorTimer += dt;
			if(m_IndicatorTimer > TimeToShowIndicator)
			{
				TopCollider.SetActive(true);
				BottomCollider.SetActive(true);
				m_state = State.MOVING_IN;
			}
		}
		else if(m_state == State.STAND)
		{
			m_StandTimer += dt;
			if(m_StandTimer > TimeStandBy)
				m_state = State.MOVING_OUT;
		}
		

	}
}
