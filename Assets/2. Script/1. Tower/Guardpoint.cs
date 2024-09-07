using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Guardpoint : MonoBehaviour
{
    [SerializeField] List<Transform> enemyInRange = new();
    [SerializeField] List<Soldier> soldiers = new();
    [SerializeField] Barrack barrack;
    [SerializeField] float colliderRange = 1f;
    CircleCollider2D circleCollider2D;

    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.radius = colliderRange;

        barrack = transform.parent.GetComponent<Barrack>();
        GetSoldierList();
    }

    void GetSoldierList()
    {
        soldiers = barrack.GetSoldierList();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("GroundEnemy"))
        {
            if(enemyInRange.Contains(other.transform)) return;
            enemyInRange.Add(other.transform);
            UpdateSoldiersEnemyInRange();
        }   
    }

    void OnTriggerExit2D(Collider2D other)
    {
        enemyInRange.Remove(other.transform);
        UpdateSoldiersEnemyInRange();
    }

    void UpdateSoldiersEnemyInRange()
    {
        foreach( var soldier in soldiers)
        {
            soldier.enemyInRange = enemyInRange;
        }
    }
}
