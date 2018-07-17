using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

	[SerializeField] Obstacle obstaclePrefab;
	[SerializeField] MinMaxFloat SpawnYRange;
	List<Obstacle> childList;
	
	// Use this for initialization
	void Awake () {
		childList = new List<Obstacle>();

		GameEvents.STAGE_CHANGED += OnStageChanged;
	}
	
	// Update is called once per frame
	void OnStageChanged () {
		StageData stageData = StageManager.Instance.CurrentStage;
		if(childList.Count < stageData.NumberOfObstacle)
		{
			int totalMissingOb = stageData.NumberOfObstacle - childList.Count;
			for(int i = 0 ; i < totalMissingOb; i ++)
			{
				childList.Add(Instantiate(obstaclePrefab.gameObject, transform).GetComponent<Obstacle>());
			}
		}
		else if (childList.Count > stageData.NumberOfObstacle)
		{
			int totalRemovedOb = stageData.NumberOfObstacle - childList.Count;
			for(int i = 0 ; i < totalRemovedOb ; i ++)
			{
				GameObject killedChild = childList[0].gameObject;
				childList.RemoveAt(0);
				Destroy(killedChild);
			}
		}

		for(int i = 0; i < childList.Count; i ++)
		{
			Obstacle ob = childList[i];

			Vector3 newPosition = ob.transform.position;
			newPosition.y = SpawnYRange.RandomValue;
			ob.transform.position = newPosition;

			ob.SetSpeed(stageData.ObstacleSpeed.RandomValue);
		}
	}
}
