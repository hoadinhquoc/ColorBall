using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	[SerializeField] float ColorChangeTime = 5f;
	[SerializeField] SpriteRenderer m_display;
	GameSetting m_gameSetting;
	
	int[] m_plaformColor;
	int m_colorIndex = 0;
	public int ColorIndex{get{return m_colorIndex;}}
	void Awake()
	{
		GameEvents.UPDATE_GAME_SETTING += OnGameSettingUpdate;
	}
	void OnDestroy()
	{
		GameEvents.UPDATE_GAME_SETTING -= OnGameSettingUpdate;
	}
	void OnGameSettingUpdate()
	{
		m_gameSetting = GameManager.Instance.Setting;
	}
	// Use this for initialization
	void Start () {
		OnGameSettingUpdate();
		ChangeColor();
	}
	
	void OnEnable()
	{
		InvokeRepeating("ChangeColor", 0f , ColorChangeTime);
	}

	void OnDisable()
	{
		CancelInvoke();
	}

	void OnPlatformColorChanged(int topColorInde, int botColorIndex)
	{

	}
	void ChangeColor()
	{
		int randomIndex = Random.Range(0, 1);

		m_colorIndex = PlatformsController.Instance.PlatformColors[randomIndex];
		m_display.color = m_gameSetting.GlobalColorList[m_colorIndex];
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("ColliderTrigger " + col.gameObject.name);
		if(col.gameObject.CompareTag("MC"))
		{
			NodeSpawner.Instance.OnNodeRemoved(this);
		}
	}
}
