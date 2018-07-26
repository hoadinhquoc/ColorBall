using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour {
	[SerializeField] AudioClip HitPlatform;
	[SerializeField] AudioClip HitObtacle;
	[SerializeField] AudioClip HitNode;

	public static Action SFX_HIT_PLATFORM;
	public static Action SFX_HIT_OBSTACLE;
	public static Action SFX_HIT_NODE;

}
