using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	MovePingPong moveComponent;
	// Use this for initialization
	void Awake () {
		moveComponent = GetComponent<MovePingPong>();
	}
	
	public void SetSpeed(float speed)
	{
		moveComponent.Speed = speed;
	}
}
