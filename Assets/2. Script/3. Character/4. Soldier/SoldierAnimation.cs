using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAnimation : CharacterAnimationBase
{
    Soldier thisSoldier;
    protected override void Start()
    {
        base.Start();
        thisSoldier = GetComponent<Soldier>();
        StartCoroutine(DoAnimation());
    }

    IEnumerator DoAnimation()
    {
        while(true)
        {
            if(thisSoldier.IsMovingToGuardPoint() || thisSoldier.IsMovingToEnemy())
            {
                base.WalkingState();
                //Debug.Log("walk");
            }
            else if(thisSoldier.IsInGuardPos())
            {
                base.IdleState();
                //Debug.Log("Idle");
            }
            else if(thisSoldier.IsEnemyInFront())
            {
                base.AttackingState();
                //Debug.Log("attack");
            }
            yield return new WaitForSeconds(0.1f);

            if(thisSoldier.IsDead())
            {
                base.DeadState();
                //Debug.Log("Dead");
                yield return new WaitUntil(() => !thisSoldier.IsDead());
            }
        }
    }
}
