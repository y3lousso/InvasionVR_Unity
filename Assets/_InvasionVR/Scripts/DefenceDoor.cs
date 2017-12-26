using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DefenceDoor : MonoBehaviour {

    public List<Enemy> attackers;
    public int hp;

	// Use this for initialization
	void Start () {
        attackers = new List<Enemy>();
    }
	
	// Update is called once per frame
	void Update () {
		if(hp == 0)
        {
            foreach(Enemy enemy in attackers)
            {
                enemy.ChangeState(EnemyState.Walking);
            }
        }
	}

    public void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.ChangeState(EnemyState.Attacking);
            attackers.Add(enemy);
        }
    }
}
