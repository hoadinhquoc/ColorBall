using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour {
	enum State{
		IDLE,
		RUNNING
	}

	[SerializeField] float Speed = 10f;

	Vector3 m_defaultPosition;
    Vector3 m_direction = Vector3.up;
	State m_state = State.IDLE;
	// Use this for initialization
	void Awake () {
		m_defaultPosition = this.transform.position;

		GameEvents.MC_CHANGE_DIRECTION += OnDirectionChange;
		GameEvents.START_GAME += OnStartGame;
	}
	void OnDirectionChange()
    {
        m_direction.y *= -1;
    }
	void OnStartGame()
	{
		m_state = State.RUNNING;
	}
	// Update is called once per frame
	void Update () {
		
		if(IsState(State.RUNNING))
			transform.position += m_direction * Speed * Time.deltaTime;

	}
	bool IsState(State state)
	{
		return state == m_state;
	}
	void Reset()
	{
		m_state = State.IDLE;
		transform.position = m_defaultPosition;
	}
	public void OnChildTriggerEnter(Collider2D col)
	{
		Debug.Log("Trigger " + col.gameObject.name);
		OnDirectionChange();
	}
}
