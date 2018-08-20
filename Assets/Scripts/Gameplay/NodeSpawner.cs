using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour {
	public static NodeSpawner Instance;
	[SerializeField] Node nodePrefab;
	[SerializeField] MinMaxFloat SpawnYRange;
    [SerializeField] float DistanceWithMC = 1f;
	[SerializeField] float timeToSpawn;
    [SerializeField] Transform MainCharacter;
	List<Node> childList;
	int numberOfNode = 2;
	
	// Use this for initialization
	void Awake () {
		Instance = this;
		childList = new List<Node>();

		GameEvents.STAGE_CHANGED += OnStageChanged;

		GameEvents.START_GAME += OnGameStart;
		GameEvents.GAME_OVER += OnGameOver;
	}
	
	// Update is called once per frame
	void OnStageChanged () {
		StageData stageData = StageManager.Instance.CurrentStage;
		
		numberOfNode = stageData.NumberOfNode;

		if(childList.Count >= numberOfNode)
		{
			int totalRemovedNode = childList.Count - numberOfNode;
			for(int i = 0; i < totalRemovedNode; i++)
				OnNodeRemoved(childList[0]);
		}
	}

	void SpawnNode()
	{
		if(childList.Count >= numberOfNode) return;
		
		Node newNode = Instantiate(nodePrefab, this.transform).GetComponent<Node>();

		Vector3 newPosition = newNode.transform.position;
        float MC_yPos = MainCharacter.position.y;
        if (MC_yPos < 0)
            newPosition.y = Random.Range(MC_yPos + DistanceWithMC, SpawnYRange.MaxValue);
        else
		    newPosition.y = Random.Range(SpawnYRange.MinValue, DistanceWithMC - MC_yPos);
        newNode.transform.position = newPosition;

		childList.Add(newNode);
	}

	void OnGameStart()
	{
		InvokeRepeating("SpawnNode", 0f, timeToSpawn);
	}

	void OnGameOver()
	{
		CancelInvoke("SpawnNode");
	}

	public void OnNodeRemoved(Node removedNode)
	{
		childList.Remove(removedNode);
		Destroy(removedNode.gameObject);
	}
}
