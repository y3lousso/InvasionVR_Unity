using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Enemy targetEnemy;
    public int damage;
    private float reachDist = .05f;
    public DamageType damageType = DamageType.Physical;
    public float lifeTime = 3f;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;
		if(targetEnemy != null)
        {
            if(Vector3.Distance(targetEnemy.transform.position, transform.position) < reachDist)
            {
                targetEnemy.ReceivedDamage(damage, damageType);
                DestroyImmediate(gameObject);
            }
            if(lifeTime<=0)
            {
                Destroy(gameObject);
            }
        }
	}
}

public enum DamageType
{
    Physical,
    Magic
}
