using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : MonoBehaviour {

    private Level level;
    private LakeManager lakeManager;
    private ForestManager forestManager;
    private MineManager mineManager;

    public GameObject towniePrefab;
    public Transform spawnLocation;

    [Header("Village spec")]
    public int hp = 100;

    [Header("Ressources")]
    public int food = 100;
    public int wood = 100;
    public int stone = 100;

    public void InitEvent()
    {
        Enemy.OnEnemyAttack += OnEnemyAttack;
    }

    public void DisableEvent()
    {
        Enemy.OnEnemyAttack -= OnEnemyAttack;
    }

    private void OnEnemyAttack(object sender, EnemyEventArgs e)
    {
        AttackReceived(e.damage);
    }

    // Use this for initialization
    void Start () {
        level = GetComponentInParent<Level>();

        lakeManager = GetComponentInChildren<LakeManager>();        
        forestManager = GetComponentInChildren<ForestManager>();
        mineManager = GetComponentInChildren<MineManager>();

        lakeManager.SetVillageManager(this);
        forestManager.SetVillageManager(this);
        mineManager.SetVillageManager(this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AttackReceived(int damage)
    {
        hp -= damage;
        CheckVillageHp();
    }

    private void CheckVillageHp()
    {
        if (hp <= 0)
        {
            level.LeveLost();
        }
    }

    public void AddNewTownie()
    {
        Instantiate(towniePrefab, spawnLocation.position, spawnLocation.rotation);
    }
}
