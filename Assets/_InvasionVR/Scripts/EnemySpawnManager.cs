using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {

    public List<EnemySpawn> enemySpawns = new List<EnemySpawn>();

    public void InitEvent()
    {

    }

    public void DisableEvent()
    {

    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartSpawn(int waveIndex)
    {
        for (int i = 0; i < enemySpawns.Count; i++)
        {
            enemySpawns[i].StartSpawn(waveIndex);
        }
    }

    public void ActualizeWaveNumber(int waveNumber)
    {
        for (int i = 0; i < enemySpawns.Count; i++)
        {
            enemySpawns[i].ActualizeWaveNumber(waveNumber);
        }
    }

}
