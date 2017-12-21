using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Enemy targetEnemy;
    public int damage;
    public DamageType damageType = DamageType.Physical;
    public float lifeTime = 10f;
    public float speed = 5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (targetEnemy.state != EnemyState.Dead && lifeTime >= 0)
        {
            Vector3 dir = (targetEnemy.transform.position + new Vector3(0, .2f, 0) - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dir);
            GetComponent<Rigidbody>().AddForce(speed * transform.forward);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() == targetEnemy)
        {
            targetEnemy.ReceivedDamage(damage, damageType);
            Destroy(gameObject);          
        }
    }

    public void OnDrawGizmos()
    {
        if(targetEnemy!=null)
            Gizmos.color = Color.red;
            Gizmos.DrawLine(targetEnemy.transform.position + new Vector3(0, .2f, 0), transform.position);
    }
}

public enum DamageType
{
    Physical,
    Magic
}
