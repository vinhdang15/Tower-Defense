using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAnimation : CharacterAnimation
{
    [SerializeField] float attackSpeed;
    Soldier soldier;
    protected override void Start()
    {
        base.Start();
        attackSpeed = 2f;
        soldier = GetComponent<Soldier>();
        StartCoroutine(DoAnimation());
    }

    IEnumerator DoAnimation()
    {
        while(true)
        {
            if(soldier.IsMovingToGuardPoint())
            {
                base.Walk();
                Debug.Log("walk");
            }
            else if(soldier.IsInStandingPos())
            {
                base.Idle();
                Debug.Log("Idle");
            }
            else if(soldier.IsInAttackPos())
            {
                base.Attack();
                Debug.Log("attack");
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
