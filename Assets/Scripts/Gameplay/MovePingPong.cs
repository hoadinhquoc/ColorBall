using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePingPong : MonoBehaviour {

	[SerializeField] public float Speed = 2f;
	[SerializeField] public Vector3 StartPoint;
	[SerializeField] public Vector3 EndPoint;

	float timer = 0f;
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
        float distance = (EndPoint - StartPoint).magnitude;
        float t = Mathf.PingPong(timer * Speed, distance) / distance;

        transform.position = Vector3.Lerp(StartPoint, EndPoint, t);
    }

    public void ResetTimer()
    {
        timer = 0f;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(StartPoint, EndPoint);
    }

}
