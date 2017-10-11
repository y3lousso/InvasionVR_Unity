using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHead : MonoBehaviour {

    private Turret turret;

	// Use this for initialization
	void Start () {
        turret = GetComponentInParent<Turret>();

    }
	
	// Update is called once per frame
	void Update () {
		if(turret.currentTarget != null)
        {
            var rotation = Quaternion.LookRotation(turret.currentTarget.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turret.rotationSpeed);
        }
	}

    public void InstaFocusEnemy()
    {
        transform.rotation = Quaternion.LookRotation(turret.currentTarget.transform.position - transform.position);
    }
}
