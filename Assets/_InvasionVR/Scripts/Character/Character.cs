using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [Header("Trigger")]
    public List<Enemy> enemiesInRange = new List<Enemy>();
    public Enemy currentTarget;
    public CharacterTrigger characterTrigger;
    public float rotationSpeed = 20f;

    [Header("Animator")]
    public Animator animator;
    //public float animationSpeed;

    [Header("Attack")]
    public GameObject projectile;
    public Transform projectileSpawn;



    protected void OnEnable()
    {
        Enemy.OnEnemyDie += OnEnemyDie;
    }

    protected void OnDisable()
    {
        Enemy.OnEnemyDie -= OnEnemyDie;
    }

    private void OnEnemyDie(object sender, EnemyEventArgs e)
    {
        enemiesInRange.Remove(e.enemy);

    }

    // Use this for initialization
    public virtual void Start()
    {
        characterTrigger = GetComponentInChildren<CharacterTrigger>();
    }

    public virtual void Update()
    {
        if (enemiesInRange.Count > 0)
        {
            animator.SetBool("IsAttacking", true);
            float closestRange = Mathf.Infinity;
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                if (enemiesInRange[i].remainingDistanceBeforeVillage < closestRange)
                {
                    closestRange = enemiesInRange[i].remainingDistanceBeforeVillage;
                    currentTarget = enemiesInRange[i];
                }
            }
            
        }
        else
        {
            animator.SetBool("IsAttacking", false);
            currentTarget = null;
        }
    }

    protected void Fire()
    {
        GameObject currentProjectile = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
        currentProjectile.transform.rotation = Quaternion.LookRotation(currentTarget.transform.position - projectile.transform.position);
        Rigidbody rbProjectile = currentProjectile.AddComponent<Rigidbody>();
        rbProjectile.isKinematic = false;
        rbProjectile.useGravity = false;
        rbProjectile.AddForce(100f * (currentTarget.transform.position - projectile.transform.position).normalized);
        projectile.GetComponent<Projectile>().targetEnemy = currentTarget;
    }
}
