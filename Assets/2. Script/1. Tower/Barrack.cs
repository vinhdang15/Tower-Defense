using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Barrack : TowerBase
{
    [Header("Tower Spec")]
    //[SerializeField] GameObject[] spawnObjectPrefabs = new GameObject[3];
    public Transform barrackRange;
    private CircleCollider2D barackRangeCol;
    public Transform guardPointBase;
    [SerializeField] List<Soldier> soldierList = new();

    [SerializeField] FlagAnimation flagAnimation;
    void Awake()
    {
        barackRangeCol = barrackRange.GetComponent<CircleCollider2D>();
        towerCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        
        currentDamage = GetCurrentDamage(currentLevel);
        currentSpawnRate = spawnRate[currentLevel];
        currentRange = detectionRanges[currentLevel];
        
        currentSpawnPrefab = spawnObjectPrefabs[currentLevel];
        GameController.Instance.SpendGold(upgradeCosts[0]);
        goldRefund += upgradeCosts[0];
    }

    void Start()
    {
        currentLevel = 0;
        barackRangeCol.radius = currentRange;
        barrackRange.gameObject.SetActive(false);
        base.SetTowerColliderRange();
        base.SetTowerBodyRadius();
        SpawnObject();
    }

    public override void SpawnObject()
    {   
        int i = 0;
        while(i < 3)
        {
            GameObject soldier = Instantiate(currentSpawnPrefab, SpawnPoint.position, Quaternion.identity, transform);
            soldier.name = "solider_" + i;
            Soldier soldierScript =  soldier.GetComponent<Soldier>();
            soldierList.Add(soldierScript);
            soldierScript.index = i;
            soldierScript.barrack = this;
            i++;
            SetSoldierGuardPoint();
        }
    }

    public List<Soldier> GetSoldierList()
    {
        return soldierList;
    }

    public void SetSoldierGuardPoint()
    {
        StartCoroutine(UpdateGuardPointCorountine());
    }

    IEnumerator UpdateGuardPointCorountine()
    {
        List<Soldier> soldierListCopy = new List<Soldier>(soldierList);
        foreach(Soldier soldierScript in soldierListCopy)
        {
            soldierScript.guardPointChild = guardPointBase;
            soldierScript.GetGuardPoints();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void UpdateGuardPointPos(Vector2 pos)
    {
        guardPointBase.position = pos;
        flagAnimation.TriggerAnimation();
    }   

    public override void Upgrade(int level)
    {
        base.Upgrade(level);
        barackRangeCol.radius = currentRange;
    }
}



