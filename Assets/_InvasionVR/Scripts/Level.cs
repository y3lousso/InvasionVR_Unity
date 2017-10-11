using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    [Header("Level Manager Info")]
    private LevelManager levelManager;
    private VillageManager villageManager;
    public EnemySpawnManager enemySpawnManager;

    [Header("Level Spec")]
    public int currentWave = 0;
    public int waveNumber;

    // Use this for initialization
    void Start () {
        levelManager = GetComponentInParent<LevelManager>();
        villageManager = GetComponentInChildren<VillageManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartLevel()
    {
        InitEvent();
        enemySpawnManager.StartSpawn(currentWave);
    }

    public void EndLevel()
    {
        DisableEvent();
    }

    private void InitEvent()
    {
        villageManager.InitEvent();
        enemySpawnManager.InitEvent();
    }

    private void DisableEvent()
    {
        villageManager.DisableEvent();
        enemySpawnManager.DisableEvent();
    }

    public void LevelCompleted()
    {
        levelManager.LevelCompleted();
    }

    public void ActualizeWaveNumber(int waveNumber)
    {
        enemySpawnManager.ActualizeWaveNumber(waveNumber);
    }

    public void LeveLost()
    {
        levelManager.LevelLost();
    }



}
