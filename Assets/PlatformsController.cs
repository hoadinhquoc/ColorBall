using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsController : MonoBehaviour {

	[SerializeField] Platform[] platforms;
	[SerializeField] GameSetting m_gameSetting;

	int numberOfColor = 2;
	void Awake()
	{
		GameEvents.START_GAME += OnGameStart;
		GameEvents.MC_COLLIDED_PLATFORM += OnMCCollidePlatform;
	}

	void OnGameStart()
	{
		m_gameSetting = GameManager.Instance.Setting;
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
	}
}
