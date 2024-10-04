using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Walk()
    {
        animator.SetFloat("Blend", 0);
    }

    protected virtual void Idle()
    {
        animator.SetFloat("Blend", 0.3f);
    }

    protected virtual void Attack()
    {
        animator.SetFloat("Blend", 0.6f);
    }

    protected virtual void Die()
    {
        animator.SetFloat("Blend", 1f);
    }

}
