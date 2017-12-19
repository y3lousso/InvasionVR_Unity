using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool enable = false;
    public Enemy targetEnemy;
    public int damage;
    public DamageType damageType = DamageType.Physical;
    public float lifeTime = 10f;
    public float arrowSpeed = 5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enable == true)
        {
            lifeTime -= Time.deltaTime;

            if (targetEnemy != null)
            {
                Vector3 dir = (targetEnemy.transform.position + new Vector3(0, .1f, 0) - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(dir);
                transform.Translate(arrowSpeed * Time.deltaTime * dir);
                if (lifeTime <= 0)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
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
}

public enum DamageType
{
    Physical,
    Magic
}
