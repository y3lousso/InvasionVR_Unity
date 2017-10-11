using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTrigger : MonoBehaviour {

    private Turret turret;
    
	// Use this for initialization
	void Start () {
        turret = GetComponentInParent<Turret>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Enemy>() != null)
        {
            turret.enemiesInRange.Add(col.GetComponent<Enemy>());
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<Enemy>() != null)
        {
            turret.enemiesInRange.Remove(col.GetComponent<Enemy>());
        }
    }

}
