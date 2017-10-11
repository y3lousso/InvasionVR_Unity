using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public List<EnemyWave> enemyWaves;
    public GameObject waves;
    public PathEditor path;
    public BezierSpline spline;

	// Use this for initialization
	void Start () {
        spline.GetComponentInChildren<BezierSpline>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartSpawn(int waveIndex)
    {
        enemyWaves[waveIndex].Spawn();
    }

    public void ActualizeWaveNumber(int waveNumber)
    {
        if(enemyWaves.Count < waveNumber) //  not enough waves
        {
            while (waveNumber > enemyWaves.Count)
            {
                GameObject go = Instantiate(new GameObject());
                go.transform.parent = waves.transform;
                go.name = ("Wave" + (enemyWaves.Count - 1).ToString());
                go.AddComponent<EnemyWave>();
            }
        }
        else if(enemyWaves.Count > waveNumber) // Too many waves
        {
            while (enemyWaves.Count > waveNumber)
            {
                enemyWaves.RemoveAt(enemyWaves.Count - 1);
            } 
        }
    }
}
