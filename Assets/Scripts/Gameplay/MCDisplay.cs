using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCDisplay : MonoBehaviour {

	[SerializeField] SpriteRenderer mainRenderer;
	[SerializeField] TrailRenderer colorTrail;
	[SerializeField] ParticleSystem DeathVFX;

	void Awake()
	{
		GameEvents.MC_BORN += OnMCBorn;
		GameEvents.MC_CHANGED_COLOR += OnMCChangedColor;
		GameEvents.MC_DEATH += OnMCDeath;
	}

	void OnMCChangedColor(Color color)
	{
		mainRenderer.color = color;

		colorTrail.startColor = color;
        color.a = 0.2f;
        colorTrail.endColor = color;
	}
	void OnMCDeath()
	{
		mainRenderer.enabled = false;
		DeathVFX.Emit(30);
	}
	void OnMCBorn()
	{
		mainRenderer.enabled = true;
	}
}
