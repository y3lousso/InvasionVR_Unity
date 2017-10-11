using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour {

    [Header("Wave Configuration")]
    public List<EnemySet> enemySets;// = new List<EnemySet>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn()
    {
        for (int i = 0; i < enemySets.Count; i++)
        {
            StartCoroutine(SpawnCoroutine(enemySets[i]));
        }
    }

    public IEnumerator SpawnCoroutine(EnemySet enemySet)
    {
        yield return new WaitForSeconds(enemySet.timeOffset);
        for (int i = 0; i < enemySet.number; i++)
        {
            Enemy enemy =  (Enemy)Instantiate(enemySet.mob, transform.GetComponentInParent<EnemySpawn>().path.waypoints[0], Quaternion.identity);
            enemy.transform.LookAt(transform.GetComponentInParent<EnemySpawn>().path.waypoints[1]);
            enemy.enemySpawn = transform.GetComponentInParent<EnemySpawn>();
            enemy.pathToFollow = transform.GetComponentInParent<EnemySpawn>().path;
            yield return new WaitForSeconds(enemySet.timeRate);
        }
    }
}

[System.Serializable]
public class EnemySet
{
    public Enemy mob;
    public int number;
    public float timeRate = 1f; // x per second
    public float timeOffset = 0f;
}