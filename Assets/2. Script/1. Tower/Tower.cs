using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : TowerBase
{
    //[Header("Tower Spec")]
    //[SerializeField] GameObject[] spawnObjectPrefabs = new GameObject[3];

    void Awake()
    {
        currentLevel = 0;
        currentDamage = GetCurrentDamage(currentLevel);
        currentSpawnRate = spawnRate[currentLevel];
        currentRange = detectionRanges[currentLevel];
        currentSpawnPrefab = spawnObjectPrefabs[currentLevel];
        GameController.Instance.SpendGold(upgradeCosts[0]);
        goldRefund += upgradeCosts[0];
    }

    void Start()
    {
        towerCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        SetTowerColliderRange();
        SetTowerBodyRadius();
        StartCoroutine(SpawnBulletCoroutine(animator));
    }

    public override void Upgrade(int level)
    {
        base.Upgrade(level);
    }
}
