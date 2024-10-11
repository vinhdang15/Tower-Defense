using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    public Soldier soldier;
    private PathFinder pathFinderScript;

    private void Awake()
    {
        pathFinderScript = GetComponent<PathFinder>();
    }
    
    private void Start()
    {
        currentPos = transform.position;
        base.SetDefaultSpeed();
        base.SetupHpCurrent();
    }

    private void Update()
    {
        Move();
        SetEnemyDirection();
    }

    protected override void Move()
    {
        if(soldier == null && !base.IsDead())
        {
            pathFinderScript.FollowPath(currentSpeed);
        }
    }

    public bool IsNonSoldier()
    {
        return soldier == null;
    }

    public bool IsSoldierApproach()
    {
        if(soldier == null) return false;
        else if(soldier.targetPosition != (Vector2)soldier.transform.position)
        {
            return true;
        }
        return false;
    }

    public bool IsSoldierInfront()
    {
        if(soldier == null) return false;
        else if(soldier.targetPosition == (Vector2)soldier.transform.position) return true;
        return false;
    }

    // Animation event
    public void AttackSoldier()
    {
        if(soldier == null) return;
        soldier.TakeDamage(damage);
    }
}
