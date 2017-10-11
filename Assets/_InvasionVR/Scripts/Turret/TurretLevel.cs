using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLevel : MonoBehaviour {

    public int price = 10;
    public int damage = 10;
    public float range = .5f;
    public float attackSpeed = 1f;

    public TurretHead turretHead;
    public GameObject projectilePrefab;
    public Transform projectileSpawn;

    // Use this for initialization
    void Start () {
        turretHead = GetComponentInChildren<TurretHead>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateLevel()
    {
        gameObject.SetActive(true);
    }

    public void DesactivateLevel()
    {
        gameObject.SetActive(false);
    }
}
