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
	[SerializeField] EventTrigger EventType;
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

	float m_TimeTriggerTimer = 0f;
    int m_ScoreTrigger = 0;
	
	// Use this for initialization
	void Awake()
	{
		GameEvents.GAME_OVER += Reset;
		GameEvents.SCORE_CHANGED += OnScoreChanged;
		Reset();
	}
	void Reset()
	{
		Hide();

		m_TimeTriggerTimer = 0f;
        m_ScoreTrigger = (int)EventType.StartValue;
	}
	void Hide()
    {
		TopLaser.gameObject.SetActive(false);
		BottomLaser.gameObject.SetActive(false);
		m_state = State.HIDE;
		m_StandTimer = 0f;
		m_IndicatorTimer = 0f;
	}
	void OnScoreChanged(int score)
    {
        if (EventType.Type == EventTriggerType.SCORE)
        {
            if (score >= m_ScoreTrigger)
            {
                Run();
                m_ScoreTrigger += (int)EventType.RepeatValue;
            }
        }
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
		GameEvents.RUN_STATIC_OBSTACLE.Raise();
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
				Hide();
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

	void LateUpdate()
    {
        if (EventType.Type == EventTriggerType.TIME && m_state == State.HIDE)
        {
            m_TimeTriggerTimer += Time.deltaTime;

            if (GameManager.Instance.SingleRunTime > EventType.StartValue)
            {
                if(m_TimeTriggerTimer > EventType.RepeatValue)
                {
                    m_TimeTriggerTimer = 0f;
                    Run();
                }
            }

        }
    }
}
