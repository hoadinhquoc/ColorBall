using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePingPong : MonoBehaviour {

	[SerializeField] float Speed = 2f;
	[SerializeField] Vector3 StartPoint;
	[SerializeField] Vector3 EndPoint;

	float timer = 0f;
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
        float distance = (EndPoint - StartPoint).magnitude;
        float t = Mathf.PingPong(timer * Speed, distance) / distance;

        transform.position = Vector3.Lerp(StartPoint, EndPoint, t);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(StartPoint, EndPoint);
    }

    private void OnGUI()
    {
        Debug.Log("OnGUI");
    }
}
