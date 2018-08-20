using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectStar : MonoBehaviour {
    SpriteRenderer m_spriteRenderer;
	// Use this for initialization
	void Awake () {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        GameEvents.MC_CHANGED_COLOR += OnMCChangedColor;
    }
	void OnDestroy()
    {
        GameEvents.MC_CHANGED_COLOR -= OnMCChangedColor;
    }
	void OnMCChangedColor(Color color)
    {
        m_spriteRenderer.color = color;
    }
}
