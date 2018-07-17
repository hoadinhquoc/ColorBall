using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour {
	enum State{
		IDLE,
		RUNNING
	}

	[SerializeField] float Speed = 10f;
	[SerializeField] SpriteRenderer m_display;
	GameSetting m_gameSetting;	
	int m_colorIndex = 0;

	Vector3 m_defaultPosition;
    Vector3 m_direction = Vector3.up;
	State m_state = State.IDLE;
	// Use this for initialization
	
	void Awake () {
		m_defaultPosition = this.transform.position;

		GameEvents.MC_CHANGE_DIRECTION += OnDirectionChange;
		GameEvents.START_GAME += OnStartGame;
	}
	void OnGameSettingUpdate()
	{
		m_gameSetting = GameManager.Instance.Setting;
	}
	// Use this for initialization
	void Start () {
		OnGameSettingUpdate();

	}

	void OnDirectionChange()
    {
        m_direction.y *= -1;
    }
	void OnStartGame()
	{
		Reset();
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
		ChangeColor(0);
	}
	public void OnChildTriggerEnter(Collider2D col)
	{
		Debug.Log("Trigger " + col.gameObject.name);

		if(col.gameObject.CompareTag("Platform"))
		{
			Platform platform = col.gameObject.GetComponent<Platform>();

			if(m_colorIndex == platform.ColorIndex)
			{
				OnDirectionChange();
				GameEvents.INSCREASE_SCORE.Raise(5);
			}
			else
			{
				CollideWrongObject();
			}
			
		}
		
		if(col.gameObject.CompareTag("Obstacle"))
		{
			CollideWrongObject();
		}

		if(col.gameObject.CompareTag("Node"))
		{
			Node node = col.gameObject.GetComponent<Node>();

			ChangeColor(node.ColorIndex);
			GameEvents.INSCREASE_SCORE.Raise(10);
		}
	}

	void ChangeColor(int colorIndex)
	{
		m_colorIndex = colorIndex;
		m_display.color = m_gameSetting.GlobalColorList[colorIndex];
	}

	void CollideWrongObject()
	{
		m_state = State.IDLE;
		GameEvents.GAME_OVER.Raise();
	}

}
