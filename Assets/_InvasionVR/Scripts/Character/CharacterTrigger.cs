using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTrigger : MonoBehaviour {

    private Character character;
    
	// Use this for initialization
	void Start () {
        character = GetComponentInParent<Character>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Enemy>() != null)
        {
            character.enemiesInRange.Add(col.GetComponent<Enemy>());
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<Enemy>() != null)
        {
            character.enemiesInRange.Remove(col.GetComponent<Enemy>());
        }
    }

}
