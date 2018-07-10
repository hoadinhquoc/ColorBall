using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPhase : MonoBehaviour {

	public void OnChangeDirectionTap()
	{
		GameEvents.MC_CHANGE_DIRECTION.Raise();
	}
}
