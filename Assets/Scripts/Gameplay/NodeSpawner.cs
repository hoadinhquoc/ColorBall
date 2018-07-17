using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour {
	public static NodeSpawner Instance;
	[SerializeField] Node nodePrefab;
	[SerializeField] MinMaxFloat SpawnYRange;
	[SerializeField] float timeToSpawn;
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
	}

	void SpawnNode()
	{
		if(childList.Count >= numberOfNode) return;
		
		Node newNode = Instantiate(nodePrefab, this.transform).GetComponent<Node>();

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
