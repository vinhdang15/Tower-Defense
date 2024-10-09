using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAnimationBase : MonoBehaviour
{
    protected Animator animator;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void ShootingState()
    {
        animator.SetTrigger("Blend");
    }
    public virtual void HitEnemyState()
    {
        animator.SetTrigger("HitEnemy");
    }
    public virtual void HitGroundState()
    {
        animator.SetTrigger("HitGround");
    }
}
