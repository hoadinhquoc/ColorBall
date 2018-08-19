using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreVFX : MonoBehaviour {
    MeshRenderer m_renderer;
    TextMesh m_textMesh;
	// Use this for initialization
	void Awake () {
        m_textMesh = GetComponent<TextMesh>();
        m_renderer = GetComponent<MeshRenderer>();
        m_renderer.enabled = false;
        GameEvents.INSCREASE_SCORE += OnIncreaseScore;
	}
	
	// Update is called once per frame
	void OnIncreaseScore(int scoreAdd) {
        m_textMesh.text = "+" + scoreAdd.ToString();
        CancelInvoke("Hide");
        Invoke("Hide", 0.5f);
        m_renderer.enabled = true;
    }

    void Hide()
    {
        m_renderer.enabled = false;
    }
}
