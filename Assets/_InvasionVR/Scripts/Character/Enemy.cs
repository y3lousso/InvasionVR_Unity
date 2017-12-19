using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyEventArgs
{
    public Enemy enemy;
    public int damage;
    public int goldOnKill;
}

public delegate void EnemyEventHandler(object sender, EnemyEventArgs e);

public class Enemy : MonoBehaviour {

    public EnemySpawn enemySpawn;

    public static event EnemyEventHandler OnEnemyAttack;
    public static event EnemyEventHandler OnEnemyDie;

    [Header("Enemy Spec")]
    public float hp = 100;
    [Range(0,100)]
    public int magicResistance = 0;
    [Range(0, 100)]
    public int armor = 0;
    public EnemyType enemyType = EnemyType.Ground;
    public float moveSpeed = .1f;
    public float attackSpeed = 1f; 
    public int damage = 1;
    public int goldOnKill = 10;

    [Header("Path")]
    public PathEditor pathToFollow;
    private bool destinationReached = false;
    public int currentWaypoint = 0;
    private float reachDist = .05f;
    public float rotationSpeed = 5.0f;
    public float remainingDistanceBeforeVillage = Mathf.Infinity;


    Vector3 lastPosition;
    Vector3 currentPosition;

    private EnemyEventArgs SetEnemyEventArgs()
    {
        EnemyEventArgs e;
        e.enemy = this;
        e.damage = damage;
        e.goldOnKill = goldOnKill;  
        return e;
    }

    public virtual void OnEnemyAttackSender(EnemyEventArgs e)                 //SENDER
    {
        if (OnEnemyAttack != null)
        {
            OnEnemyAttack(this, e);
        }
    }

    public virtual void OnEnemyDieSender(EnemyEventArgs e)                 //SENDER
    {
        if (OnEnemyDie != null)
        {
            OnEnemyDie(this, e);
        }
    }


    // Use this for initialization
    void Start () {
        lastPosition = transform.position;
	}

    // Update is called once per frame
    void Update()
    {
        if (!destinationReached)
        {
            float distance = Vector3.Distance(pathToFollow.waypoints[currentWaypoint], transform.position);
            transform.position = Vector3.MoveTowards(transform.position, pathToFollow.waypoints[currentWaypoint], Time.deltaTime * moveSpeed);

            var rotation = Quaternion.LookRotation(pathToFollow.waypoints[currentWaypoint] - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

            remainingDistanceBeforeVillage = Vector3.Distance(pathToFollow.waypoints[currentWaypoint], transform.position);
            for (int i = currentWaypoint; i < pathToFollow.waypoints.Count-1; i++)
            {
                remainingDistanceBeforeVillage += pathToFollow.distanceBetweenWaypoints[i];
            }

            if (distance <= reachDist)
            {
                currentWaypoint++;
            }

            if (currentWaypoint >= pathToFollow.waypoints.Count)
            {
                EnemyReachVillage();
                destinationReached = true;
            }
        }
    }



    public void EnemyReachVillage()
    {
        // emit event
        StartCoroutine(AttackVillage());
    }

    public void KillEnemy()
    {
        // emit enemy death (gold)
        OnEnemyDieSender(SetEnemyEventArgs());
        StopCoroutine(AttackVillage());
        Destroy(gameObject);
    }

    private IEnumerator AttackVillage()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / attackSpeed);
            // Attack animation
            OnEnemyAttackSender(SetEnemyEventArgs());
        }
    }

    public void ReceivedDamage(int damage, DamageType damageType)
    {
        
        if(damageType == DamageType.Physical)
        {
            hp -=(int)(damage * (1 - armor / 100f));
        }
        else if(damageType == DamageType.Magic)
        {
            hp -= (int)(damage * (1 - magicResistance / 100f));
        }
        if (hp <= 0)
        {
            OnEnemyDieSender(SetEnemyEventArgs());
            //DeathAnimation
            Destroy(gameObject, .1f);
        }
    }

}

public enum EnemyType
{
    Ground,
    Air
}
