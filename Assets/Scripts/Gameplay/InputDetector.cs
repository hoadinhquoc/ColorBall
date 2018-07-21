using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDetector : MonoBehaviour {

	void Awake()
	{
		Input.simulateMouseWithTouches = true;
	}
	// Use this for initialization
	enum State
	{
		RELEASE,
		DOWN
	}

	State m_state = State.RELEASE;

	bool IsState(State state)
	{
		return m_state == state;
	}

	void SetState(State state)
	{
		m_state = state;
	}
	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(IsState(State.RELEASE))
			{
				GameEvents.MC_CHANGE_DIRECTION.Raise();
			}

			SetState(State.DOWN);
		}
		else
		{
			SetState(State.RELEASE);
		}
	}
}
