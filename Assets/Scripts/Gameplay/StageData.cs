using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MinMaxFloat
{
	public float MinValue = 0f;
	public float MaxValue = 5f;

	public float RandomValue{get{return Random.Range(MinValue,MaxValue);}}

	public MinMaxFloat()
	{

	}
}

[CreateAssetMenu]
public class StageData : ScriptableObject {

	public int PassingScore = 25;

	[Header("Obstacle")]
	public int NumberOfObstacle = 0;
	public MinMaxFloat ObstacleSpeed;

	[Header("Node")]
	public int NumberOfNode = 0;
}
