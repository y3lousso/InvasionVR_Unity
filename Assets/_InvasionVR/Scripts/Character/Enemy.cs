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

    [Header("State")]
    public EnemyState state;
    public Animator animator;


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
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!destinationReached && state == EnemyState.Walking)
        {
            // Walking routine
            WalkingAlongPath();

            // If the enemy reach the village
            if (currentWaypoint >= pathToFollow.waypoints.Count)
            {
                OnEnemyAttackSender(SetEnemyEventArgs());
                destinationReached = true;
            }
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
            StartCoroutine("DeathCoroutine");
        }
    }



    public void ChangeState(EnemyState _state)
    {
        switch (_state)
        {
            case (EnemyState.Walking):
                state = _state;
                animator.SetBool("IsAttacking", false);
                break;
            case (EnemyState.Attacking):
                state = _state;
                animator.SetBool("IsAttacking", true);
                break;
            case (EnemyState.Dead):
                state = _state;
                animator.SetBool("IsAttacking", false);
                animator.SetBool("IsDead", true);
                break;
            default:
                new System.Exception("Undefined state.");
                break;
        }
    }

    private void WalkingAlongPath()
    {
        float distance = Vector3.Distance(pathToFollow.waypoints[currentWaypoint], transform.position);
        transform.position = Vector3.MoveTowards(transform.position, pathToFollow.waypoints[currentWaypoint], Time.deltaTime * moveSpeed);

        var rotation = Quaternion.LookRotation(pathToFollow.waypoints[currentWaypoint] - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        remainingDistanceBeforeVillage = Vector3.Distance(pathToFollow.waypoints[currentWaypoint], transform.position);
        for (int i = currentWaypoint; i < pathToFollow.waypoints.Count - 1; i++)
        {
            remainingDistanceBeforeVillage += pathToFollow.distanceBetweenWaypoints[i];
        }
        if (distance <= reachDist)
        {
            currentWaypoint++;
        }
    }

    public IEnumerator DeathCoroutine()
    {
        OnEnemyDieSender(SetEnemyEventArgs());
        ChangeState(EnemyState.Dead);
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(2);
        float t = 0;
        float timeStep = 0.05f;
        while (t <= 1f)
        {
            yield return new WaitForSeconds(timeStep);
            t += timeStep;
            transform.position -= new Vector3(0, 0.1f * timeStep, 0);
        }
        Destroy(gameObject);
        yield return null;
    }


}

public enum EnemyType
{
    Ground,
    Air
}

public enum EnemyState
{
    Walking,
    Attacking,
    Dead
}
