using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    [SerializeField] int enemyGold;
    public Soldier soldier;
    
    void Start()
    {
        currentPos = transform.position;
        SetupHpCurrent();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        SetEnemyDirection();
    }

    public override void Move()
    {
        if(GameController.Instance.GetGameOverStatus()) return;
        if(soldier == null)
        {
            animator.ResetTrigger("isAttack");
            base.Move();
            AnimationBlendState(false);
        }
        else CheckToAnimationFightingState();
    }

    public override void Die()
    {
        GameController.Instance.AddGold(enemyGold);
        Destroy(gameObject);
    }

    public override void CheckToAnimationFightingState()
    {
        //if(soldier == null) return;
        AnimationBlendState(true);
        if(soldier.targetPosition == (Vector2)soldier.transform.position)
        {
            animator.SetTrigger("isAttack");            
        }
    }

    public void HitSoldier()
    {
        if(soldier == null) return;
        soldier.TakeDamage(damage);
    }
}
