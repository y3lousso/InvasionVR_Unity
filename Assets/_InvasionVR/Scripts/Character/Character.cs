using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [Header("Trigger")]
    public List<Enemy> enemiesInRange = new List<Enemy>();
    public Enemy currentTarget;
    private CharacterTrigger characterTrigger;
    public float rotationSpeed = 20f;

    //[Header("Animator")]
    private Animator animator;
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
        animator = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        if (enemiesInRange.Count > 0)
        {
            animator.SetBool("IsAttacking", true);
            SelectClosestTarget();           
            var rotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position);
            rotation.x = 0;
            rotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
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
        projectile.GetComponent<Projectile>().targetEnemy = currentTarget;
        Debug.Log(currentTarget.name);
        Debug.Log(projectile.GetComponent<Projectile>().targetEnemy.name);
        projectile.GetComponent<Projectile>().enable = true;
    }

    private void SelectClosestTarget()
    {
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

}
