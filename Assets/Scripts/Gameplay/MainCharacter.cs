using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour {

	[SerializeField] float Speed = 10f;

    Vector3 m_direction = Vector3.up;

	// Use this for initialization
	void Start () {
		GameEvents.MC_CHANGE_DIRECTION += OnDirectionChange;
	}
	void OnDirectionChange()
    {
        m_direction.y *= -1;
    }
	// Update is called once per frame
	void Update () {
		
		transform.position += m_direction * Speed * Time.deltaTime;

	}

	public void OnChildTriggerEnter(Collider2D col)
	{
		Debug.Log("Trigger " + col.gameObject.name);
		OnDirectionChange();
	}
}
