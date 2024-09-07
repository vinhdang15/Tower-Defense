using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAnimation : MonoBehaviour
{
    [SerializeField] Vector2 currentPos;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerAnimation()
    {
        animator.SetTrigger("IsUpdatePos");
    }
}
