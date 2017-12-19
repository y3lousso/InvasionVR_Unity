using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Townie : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FishingEnd()
    {
        Debug.Log("fishing");
    }

    public void ChoppingEnd()
    {
        Debug.Log("chopping");
    }

    public void MiningEnd()
    {
        Debug.Log("mining");
    }
}
