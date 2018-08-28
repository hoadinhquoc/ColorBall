using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSyncVFX : MonoBehaviour {

	// Use this for initialization
	ParticleSystem m_particleSys;
	void Awake () {
		m_particleSys = GetComponent<ParticleSystem>();

		GameEvents.MC_CHANGED_COLOR += OnMCChangedColor;
	}
	
	// Update is called once per frame
	void OnMCChangedColor (Color mcColor) {
		m_particleSys.startColor = mcColor;
	}
}
