using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderTrigger : MonoBehaviour {

    private Defender defender;
    
	// Use this for initialization
	void Start () {
        defender = GetComponentInParent<Defender>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Enemy>() != null)
        {
            defender.enemiesInRange.Add(col.GetComponent<Enemy>());
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<Enemy>() != null)
        {
            defender.enemiesInRange.Remove(col.GetComponent<Enemy>());
        }
    }

}
