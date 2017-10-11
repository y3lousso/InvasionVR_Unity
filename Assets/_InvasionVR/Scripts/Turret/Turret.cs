using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    [Header("Turret levels")]
    public List<TurretLevel> turretLevels;
    public int currentLevel = 0;

    [Header("Turret trigger")]
    public List<Enemy> enemiesInRange = new List<Enemy>();
    public Enemy currentTarget;
    public TurretTrigger turretTrigger;
    public float rotationSpeed = 20f;

    [Header("Turret Attack")]
    public float timeBeforeNextShot;

    [Header("Turret state")]
    public bool isActivated = false;

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
    public virtual void Start () {
        turretTrigger = GetComponentInChildren<TurretTrigger>();
        BuyTurret();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (isActivated == true && enemiesInRange.Count > 0)
        {
            timeBeforeNextShot -= Time.deltaTime;
            float closestRange = Mathf.Infinity;
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                if(enemiesInRange[i].remainingDistanceBeforeVillage < closestRange)
                {
                    closestRange = enemiesInRange[i].remainingDistanceBeforeVillage;
                    currentTarget = enemiesInRange[i];
                }
            }
            if(timeBeforeNextShot<= 0f)
            {
                Fire();
                timeBeforeNextShot = 1 / turretLevels[currentLevel].attackSpeed;
            }
        }else
        {
            currentTarget = null;
        }


    }

    protected void Fire()
    {
        GameObject projectile = Instantiate(turretLevels[currentLevel].projectilePrefab, turretLevels[currentLevel].projectileSpawn.position, turretLevels[currentLevel].projectileSpawn.rotation);
        projectile.transform.rotation = Quaternion.LookRotation(currentTarget.transform.position - projectile.transform.position);
        Rigidbody rbProjectile = projectile.AddComponent<Rigidbody>();
        rbProjectile.isKinematic = false;
        rbProjectile.useGravity = false;
        rbProjectile.AddForce(100f * (currentTarget.transform.position - projectile.transform.position).normalized);
        projectile.GetComponent<Projectile>().targetEnemy = currentTarget;
    }

    public void BuyTurret()
    {
        UpdateLevel();
        Activate();
    }

    public void UpgradeTurret()
    {
        if (currentLevel < turretLevels.Count)
        {
            currentLevel++;
            UpdateLevel();
        }
        else
        {
            Debug.Log("Max level reached");
        }
    }

    public void SellTurret()
    {
        Desactivate();
    }

    public void Activate()
    {
        isActivated = true;
    }

    public void Desactivate()
    {
        isActivated = false;
    }

    protected void UpdateLevel()
    {
        for (int i = 0; i < turretLevels.Count; i++)
        {
            turretLevels[i].DesactivateLevel();
        }
        turretLevels[currentLevel].ActivateLevel();
        timeBeforeNextShot = 1 / turretLevels[currentLevel].attackSpeed;
        UpdateTrigger();
    }

    private void UpdateTrigger()
    {
        float scale = turretLevels[currentLevel].range;
        turretTrigger.transform.localScale = new Vector3(scale, scale, scale);
    }

}


