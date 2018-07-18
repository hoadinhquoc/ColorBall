using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : MonoBehaviour {

	[SerializeField] public float Speed = 2f;
	[SerializeField] public Vector3 StartPoint;
	[SerializeField] public Vector3 EndPoint;
    System.Action EndCallback;
	float timer = 0f;
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
        float distance = (EndPoint - StartPoint).magnitude;
        float t = timer * Speed / distance;

        transform.position = Vector3.Lerp(StartPoint, EndPoint, t);

        if(t > 1f && EndCallback != null)
           EndCallback();
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

    public void SetEndCallback(System.Action action)
    {
        EndCallback = action;
    }
}
