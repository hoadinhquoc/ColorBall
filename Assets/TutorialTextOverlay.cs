using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextOverlay : MonoBehaviour {
    [SerializeField] Text TutorialText;
	// Use this for initialization
	void Awake () {
        TutorialText.enabled = false;
        TutorialManager.SHOW_TUTORIAL_TEXT += OnShowTutorialText;
        TutorialManager.HIDE_TUTORIAL_TEXT += OnHideTutorialText;
    }
	
	// Update is called once per frame
	void OnShowTutorialText(string text) {
        TutorialText.enabled = true;
	}

    void OnHideTutorialText()
    {
        TutorialText.enabled = false;
    }
}
