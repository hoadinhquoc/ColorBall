using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public enum Indentify
	{
		TOP = 0,
		BOTTOM
	}
	[SerializeField] Indentify indentify;
	public Indentify PlatformIndentify{get{return indentify;}}
	[SerializeField] SpriteRenderer m_display;
	GameSetting m_gameSetting;
	
	int m_colorIndex = 0;
	public int ColorIndex{get{return m_colorIndex;}}
	void Awake()
	{
		GameEvents.UPDATE_GAME_SETTING += OnGameSettingUpdate;
	}

	void OnGameSettingUpdate()
	{
		m_gameSetting = GameManager.Instance.Setting;
	}
	// Use this for initialization
	void Start () {
		OnGameSettingUpdate();
		ChangeColor((int)indentify);
	}

	public void ChangeColor(int colorIndex)
	{
		m_colorIndex = colorIndex;
		m_display.color = m_gameSetting.GlobalColorList[colorIndex];
	}
}
