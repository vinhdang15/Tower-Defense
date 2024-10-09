using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyAnimation : CharacterAnimationBase
{
    WalkingEnemy thisEnemy;
    protected override void Start()
    {
        base.Start();
        thisEnemy = GetComponent<WalkingEnemy>();
        StartCoroutine(DoAnimation());
    }

    IEnumerator DoAnimation()
    {
        while(!thisEnemy.IsDead())
        {
            if(thisEnemy.NonSoldier())
            {
                base.WalkingState();
                //Debug.Log("walk");
            }
            else if(thisEnemy.IsSoldierApproach())
            {
                base.IdleState();
                //Debug.Log("Idle");
            }
            else if(thisEnemy.IsSoldierInfront())
            {
                base.AttackingState();
                Debug.Log("attack");
            }
            yield return new WaitForSeconds(0.1f);       
        }

        if(thisEnemy.IsDead())
        {
            base.DeadState();
            //Debug.Log("Dead");
        }
    }
}
