using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsController : MonoBehaviour {
	public static PlatformsController Instance;
	[SerializeField] Platform[] platforms;
	//[SerializeField] GameSetting m_gameSetting;
	int[] m_platformColors;
	public int[] PlatformColors{get{return m_platformColors;}}
	int numberOfColor = 2;
	void Awake()
	{
		Instance = this;
		m_platformColors = new int[2];

		GameEvents.START_GAME += OnGameStart;
		GameEvents.MC_COLLIDED_PLATFORM += OnMCCollidePlatform;
		GameEvents.STAGE_CHANGED += OnStageChanged;
	}

	void OnGameStart()
	{
		//m_gameSetting = GameManager.Instance.Setting;

		platforms[0].ChangeColor(1);
		platforms[1].ChangeColor(0);

		m_platformColors[0] = 1;
		m_platformColors[1] = 0;
	}

	void OnStageChanged()
	{
		numberOfColor = StageManager.Instance.CurrentStage.NumberOfColor;
	}

	void OnMCCollidePlatform(Platform.Indentify indentify)
	{
		Platform collidedPlatform = platforms[(int)indentify];
		int collidedColorIndex = collidedPlatform.ColorIndex;

		Platform anotherPlatform;
		if(indentify == Platform.Indentify.TOP)
			anotherPlatform = platforms[(int)Platform.Indentify.BOTTOM];
		else
			anotherPlatform = platforms[(int)Platform.Indentify.TOP];


		List<int> colorIndexList = new List<int>();
		for(int i = 0 ; i < numberOfColor; i++)
		{
			if(i != collidedColorIndex)
				colorIndexList.Add(i);
		}
		
		int randomIndex = Random.Range(0,colorIndexList.Count);

		collidedPlatform.ChangeColor(colorIndexList[randomIndex]);

		colorIndexList.RemoveAt(randomIndex);
		colorIndexList.Add(collidedColorIndex);

		randomIndex = Random.Range(0,colorIndexList.Count);
		anotherPlatform.ChangeColor(colorIndexList[randomIndex]);

		m_platformColors[0] = platforms[0].ColorIndex;
		m_platformColors[1] = platforms[1].ColorIndex;
	}

	
}
