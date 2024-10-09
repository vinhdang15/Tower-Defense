using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationBase : MonoBehaviour
{
    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void WalkingState()
    {
        animator.SetFloat("Blend", 0);
    }

    protected virtual void IdleState()
    {
        animator.SetFloat("Blend", 0.3f);
    }

    protected virtual void AttackingState()
    {
        animator.SetFloat("Blend", 0.6f);
    }

    protected virtual void DeadState()
    {
        animator.SetTrigger("Dead");
    }

}
