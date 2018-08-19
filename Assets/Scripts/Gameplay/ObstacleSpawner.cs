using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;
    [SerializeField] Obstacle obstaclePrefab;
    [SerializeField] MinMaxFloat SpawnYRange;
    [SerializeField] float timeToSpawn;
    int numberOfObstacle = 0;
    List<Obstacle> childList;
    StageData stageData;
    // Use this for initialization
    void Awake()
    {
        Instance = this;
        childList = new List<Obstacle>();

        GameEvents.START_GAME += OnGameStart;
        GameEvents.GAME_OVER += OnGameOver;
        GameEvents.STAGE_CHANGED += OnStageChanged;

        GameEvents.RUN_STATIC_OBSTACLE += StopSpawn;
        GameEvents.END_STATIC_OBSTACLE += StartSpawn;
    }

    // Update is called once per frame
    void OnStageChanged()
    {
        stageData = StageManager.Instance.CurrentStage;
        numberOfObstacle = stageData.NumberOfObstacle;
    }
    void StopSpawn()
    {
        CancelInvoke("SpawnChild");
    }
    void StartSpawn()
    {
        CancelInvoke("SpawnChild");
        InvokeRepeating("SpawnChild", 0, timeToSpawn);
    }
    void OnGameStart()
    {
        CancelInvoke("SpawnChild");
        InvokeRepeating("SpawnChild", 0, timeToSpawn);
    }
    void OnGameOver()
    {
        CancelInvoke("SpawnChild");
        for (int i = 0; i < childList.Count; i++)
        {
            Destroy(childList[i].gameObject);
        }

        childList.Clear();
    }

    void SpawnChild()
    {
        if (childList.Count < numberOfObstacle)
        {
            //int totalMissingOb = numberOfObstacle - childList.Count;

            Obstacle ob = Instantiate(obstaclePrefab.gameObject, transform).GetComponent<Obstacle>();

            childList.Add(ob);

            ob.SetSpeed(stageData.ObstacleSpeed.RandomValue);
            ob.SetPositionY(SpawnYRange.RandomValue);
            ob.Run();

        }


    }

    public void OnChildRemoved(Obstacle ob)
    {
        childList.Remove(ob);
        Destroy(ob.gameObject);
    }
}
