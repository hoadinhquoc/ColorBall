using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    [System.Serializable]
    class TutorialSave
    {
        public List<TUTORIAL_STEP> CompleteTutorialList;
        public TutorialSave()
        {
            CompleteTutorialList = new List<TUTORIAL_STEP>();
        }
    }
    TutorialSave m_data;
    public static TutorialManager Instance;
    void LoadSave()
    {
        string saveString = PlayerPrefs.GetString("Tutorial", string.Empty);

        if (string.IsNullOrEmpty(saveString))
        {
            m_data = new TutorialSave();
        }
        else
        {
            m_data = JsonUtility.FromJson<TutorialSave>(saveString);
        }
    }
    string GetSaveString()
    {
        return JsonUtility.ToJson(m_data);
    }
    public void Save()
    {
        PlayerPrefs.SetString("Tutorial", GetSaveString());
    }

    public enum TUTORIAL_STEP
    {
        HIT_FLATFORM = 0,
        COLOR_NODE,
        COUNT
    }

    TUTORIAL_STEP m_currentTutorialStep = TUTORIAL_STEP.COUNT;
    bool[] TutorialsFinish;

    [SerializeField] GameObject TutorialHand;
    [SerializeField] float TimeToShowHand = 0.5f;
    void Awake()
    {
        Instance = this;
        Init();
        GameEvents.START_GAME += OnGameStart;
    }
    public void SetFinishTutorial(TUTORIAL_STEP step)
    {
        m_currentTutorialStep = TUTORIAL_STEP.COUNT;
        m_data.CompleteTutorialList.Add(step);
        UpdateTutorialStatus();
        Save();
    }

    public bool IsFinishTutorial(TUTORIAL_STEP step)
    {
        return TutorialsFinish[(int)step];
    }

    public void ResetTutorial()
    {
        m_data.CompleteTutorialList.Clear();
        UpdateTutorialStatus();

        Save();
    }

    public void ResetTutorial(TUTORIAL_STEP step)
    {
        if (m_data.CompleteTutorialList.Exists(x => x == step))
        {
            m_data.CompleteTutorialList.Remove(step);
            UpdateTutorialStatus();
        }
    }

    void Init()
    {
        LoadSave();
        TutorialsFinish = new bool[(int)TUTORIAL_STEP.COUNT];
        UpdateTutorialStatus();
    }

    void UpdateTutorialStatus()
    {
        for (int i = 0; i < TutorialsFinish.Length; i++)
        {
            TutorialsFinish[i] = false;
        }
        for (int i = 0; i < m_data.CompleteTutorialList.Count; i++)
        {
            TutorialsFinish[(int)m_data.CompleteTutorialList[i]] = true;
        }
    }
    void OnGameStart()
    {
        if(!IsFinishTutorial(TUTORIAL_STEP.HIT_FLATFORM))
        {
            Invoke("ShowTutorialHitPlatform", TimeToShowHand);
        }
    }

    //
    void ShowTutorialHitPlatform()
    {
        Time.timeScale = 0.2f;
        TutorialHand.SetActive(true);
        GameEvents.MC_CHANGE_DIRECTION += HideTutotiralHitPlatform;
    }
    void HideTutotiralHitPlatform()
    {
        Time.timeScale = 1f;
        TutorialHand.SetActive(false);
        SetFinishTutorial(TUTORIAL_STEP.HIT_FLATFORM);
        GameEvents.MC_CHANGE_DIRECTION -= HideTutotiralHitPlatform;
    }
}
