using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyColliderEvent : UnityEvent<Collider2D>
{

}
public class ColliderTrigger : MonoBehaviour {

	[SerializeField] MyColliderEvent TriggerEnterEvent;
	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("ColliderTrigger " + col.gameObject.name);
		TriggerEnterEvent.Invoke(col);
	}
}
