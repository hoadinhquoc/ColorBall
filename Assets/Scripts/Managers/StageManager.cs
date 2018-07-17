using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
	public static StageManager Instance;
	[SerializeField] List<StageData> StageList;
	// Use this for initialization
	int m_currentStageIndex = 0;
	int m_targetScoreToChangeStage = 0;
	StageData m_currentStage;
	public StageData CurrentStage {get {return m_currentStage;}}
	void Awake () {
		Instance = this;

		GameEvents.SCORE_CHANGED += OnScoreInscrease;
		GameEvents.START_GAME += OnGameStart;
		
	}
	
	void OnScoreInscrease (int currentScore) {
		if(currentScore >= m_targetScoreToChangeStage && currentScore > 0)
			ChangeStage(++m_currentStageIndex);
	}

	void OnGameStart()
	{	
		m_currentStageIndex = 0;
		ChangeStage(0);
	}
	void ChangeStage(int stageIndex)
	{
		int index = Mathf.Min(stageIndex, StageList.Count - 1);
		m_currentStage = StageList[index];
		if(index == 0)
			m_targetScoreToChangeStage = m_currentStage.PassingScore;
		else 
			m_targetScoreToChangeStage += m_currentStage.PassingScore;
		
		GameEvents.STAGE_CHANGED.Raise();
	}
}
