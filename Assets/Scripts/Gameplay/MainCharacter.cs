using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour {
	enum State{
		IDLE,
		RUNNING
	}
	enum MyGameObject
	{
		Platform = 0,
		Obstacle = 1,
		Node = 2,
		Count
	}
	[SerializeField] float Speed = 10f;
	[SerializeField] SpriteRenderer m_display;
	[SerializeField] List<AudioClip> sfxList;
	[SerializeField] AudioSource speaker;
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
		{
			transform.position += m_direction * Speed * Time.deltaTime;
			GameEvents.MC_CHANGED_POSITION.Raise(transform.position);
		}
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
				GameEvents.INSCREASE_SCORE.Raise(1);
				GameEvents.MC_COLLIDED_PLATFORM.Raise(platform.PlatformIndentify);
			}
			else
			{
				CollideWrongObject();
			}
			
			PlaySFX(sfxList[(int)MyGameObject.Platform]);
		}

        if (col.gameObject.CompareTag("Perfect"))
        {
            Destroy(col.gameObject);
           
            GameEvents.INSCREASE_SCORE.Raise(2);
            
        }

        if (col.gameObject.CompareTag("Obstacle"))
		{
			CollideWrongObject();
			PlaySFX(sfxList[(int)MyGameObject.Obstacle]);
		}

		if(col.gameObject.CompareTag("Node"))
		{
			Node node = col.gameObject.GetComponent<Node>();

			ChangeColor(node.ColorIndex);
			PlaySFX(sfxList[(int)MyGameObject.Node]);
		}
	}

	void ChangeColor(int colorIndex)
	{
		m_colorIndex = colorIndex;
		m_display.color = m_gameSetting.GlobalColorList[colorIndex];
        GameEvents.MC_CHANGED_COLOR.Raise(m_gameSetting.GlobalColorList[colorIndex]);

    }

	void CollideWrongObject()
	{
		m_state = State.IDLE;
		GameEvents.GAME_OVER.Raise();
	}

	void PlaySFX(AudioClip clip)
	{
		speaker.clip = clip;
		speaker.Play();
	}
}
