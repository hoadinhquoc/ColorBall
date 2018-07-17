using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour {

	TextMeshProUGUI textMesh;
	// Use this for initialization
	void Awake () {
		GameEvents.SCORE_CHANGED += UpdateScoreText;
		textMesh = GetComponent<TextMeshProUGUI>();

		UpdateScoreText(GameManager.Instance.Score);
	}
	
	// Update is called once per frame
	void UpdateScoreText (int score) {
		textMesh.text = score.ToString();
	}
}
