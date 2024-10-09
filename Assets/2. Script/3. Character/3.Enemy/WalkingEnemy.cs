using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    public Soldier soldier;
    
    void Start()
    {
        currentPos = transform.position;
        SetupHpCurrent();
    }

    void Update()
    {
        Move();
        SetEnemyDirection();
    }

    public override void Move()
    {
        if(soldier == null && !base.IsDead())
        {
            base.Move();
        }
    }

    public bool NonSoldier()
    {
        return soldier == null;
    }
    public bool IsSoldierApproach()
    {
        if(soldier == null) return false;
        else if(soldier.targetPosition != (Vector2)soldier.transform.position)
        {
            Debug.Log($"{soldier.targetPosition} + {(Vector2)soldier.transform.position}");
            return true;
        }
        return false;
    }

    public bool IsSoldierInfront()
    {
        Debug.Log("there is soldier");
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
