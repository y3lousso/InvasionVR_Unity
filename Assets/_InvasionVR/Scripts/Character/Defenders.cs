﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Townie
{

    [Header("Trigger")]
    public List<Enemy> enemiesInRange = new List<Enemy>();
    public Enemy currentTarget;
    private DefenderTrigger defenderTrigger;
    public float rotationSpeed = 20f;

    //[Header("Animator")]
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
    protected override void Start()
    {
        base.Start();
        defenderTrigger = GetComponentInChildren<DefenderTrigger>();
    }

    protected override void Update()
    {
        base.Update();
        if (enemiesInRange.Count > 0)
        {
            animator.SetBool("IsAttacking", true);
            SelectClosestTarget();
            if (currentTarget != null)
            {
                var rotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position);
                rotation.x = 0;
                rotation.z = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
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
        currentProjectile.GetComponent<Projectile>().targetEnemy = currentTarget;
    }

    private void SelectClosestTarget()
    {
        float closestRange = Mathf.Infinity;
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            if (enemiesInRange[i].remainingDistanceBeforeVillage < closestRange && enemiesInRange[i].state != EnemyState.Dead)
            {
                closestRange = enemiesInRange[i].remainingDistanceBeforeVillage;
                currentTarget = enemiesInRange[i];
            }
        }
    }



}
