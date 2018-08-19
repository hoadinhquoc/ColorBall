﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartObstacle : MonoBehaviour
{
    enum State
    {
        HIDE,
        SHOW_INDICATOR,
        FIRE
    }
    [SerializeField] GameObject IndicatorContainer;
    [SerializeField] GameObject LeftIndicator;
    [SerializeField] GameObject RightIndicator;
    [SerializeField] GameObject RealObstacle;
    [SerializeField] float Speed = 3f;
    [SerializeField] float TimeToFireAfterIndicatorAppear = 2f;
    [SerializeField] float DefaultXPosition = 4f;
    State m_state = State.HIDE;
    float m_timer = 0f;
    bool m_IsRightToLeft = true;
    bool m_isFired = false;

    // Use this for initialization
    void Awake()
    {
        GameEvents.MC_CHANGED_POSITION += OnMCChangedPosition;
        GameEvents.START_GAME += OnGameStart;
        GameEvents.GAME_OVER += Reset;

        GameEvents.RUN_SMART_OBSTACLE += Run;
    }
    void OnGameStart()
    {
        IndicatorContainer.SetActive(false);
        RealObstacle.SetActive(false);
    }
    void Reset()
    {
        m_state = State.HIDE;
		IndicatorContainer.SetActive(false);
		RealObstacle.SetActive(false);
		m_timer = 0f;
    }

    void Run()
    {
        m_timer = 0f;
        IndicatorContainer.SetActive(true);
		m_state = State.SHOW_INDICATOR;
        //Choose L or R
        m_IsRightToLeft = Time.frameCount % 2 == 0;
        LeftIndicator.SetActive(!m_IsRightToLeft);
        RightIndicator.SetActive(m_IsRightToLeft);
        RealObstacle.transform.localPosition = new Vector3(m_IsRightToLeft ? DefaultXPosition : -DefaultXPosition, 0f, 0f);
    }

    // Update is called once per frame
    void OnMCChangedPosition(Vector3 MC_Position)
    {

        if (m_state != State.HIDE)
        {
            float dt = Time.deltaTime;

            m_timer += dt;

            if (m_timer <= TimeToFireAfterIndicatorAppear)
            {
                Vector3 position = transform.position;
                position.y = MC_Position.y;

                transform.position = position;
            }
            else
            {
                if (m_state == State.FIRE)
                {
                    MovingRealObstacle(dt);
                }
                else
                {
                    RealObstacle.SetActive(true);
                    
					m_state = State.FIRE;
                }
            }
        }
    }

    void MovingRealObstacle(float dt)
    {
        Vector3 localPos = RealObstacle.transform.localPosition;

        localPos.x += Speed * dt * (m_IsRightToLeft ? -1f : 1f);
        RealObstacle.transform.localPosition = localPos;

        if(localPos.x > DefaultXPosition || localPos.x < -DefaultXPosition)
			Reset();
    }
}
