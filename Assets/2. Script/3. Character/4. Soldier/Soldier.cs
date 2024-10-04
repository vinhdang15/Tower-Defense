using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Soldier : Character
{
    // ======= SOLDIER STATUS =======
    public int index;
    public bool isDead = false;
    public Barrack barrack;

    // ======= SOLDIER ANIMATION =======
    SpriteRenderer spriteRenderer;
    Vector2 currentLocalScale = new();

    // ======= SOLDIER TARGET GUARD POINT =======
    [SerializeField] public Transform guardPointChild;
    [SerializeField] Vector2 guardPointPos;
    [SerializeField] bool moveToGuardPoint = true;

    // ======= SOLDIER TARGET ENEMY =======
    public Transform targetEnemy;
    public Vector2 targetPosition;
    public List<Transform> enemyInRange = new();
    public bool hadTarget = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        currentLocalScale = transform.localScale;
        SetupHpCurrent();
    }
    void Update()
    {
        Move();
    }

    public override void Move()
    {
        if(isDead) return;
        
        if(IsMovingToGuardPoint())
        {
            UnTargetEnemy();
            MoveProcess();
            SetSoldierDirect(guardPointPos);
        }
        else
        {
            moveToGuardPoint = false;
            SetTargetEnemy();
            MoveToTargetEnemy();
            CheckToAnimationFightingState();
            
            if(targetEnemy != null)
            {
                SetSoldierDirect(targetEnemy.position);
            }          
        }

        // if(transform.position == (Vector3)guardPointPos || 
        //    targetEnemy != null && transform.position == (Vector3)targetPosition)
        // {
        //     AnimationBlendState(true);
        // }
        // else  
        // {
        //     AnimationBlendState(false);
        // }
    }

    public bool IsMovingToGuardPoint()
    {
        if(transform.position != (Vector3)guardPointPos)
            if(moveToGuardPoint == true || enemyInRange.Count == 0)
            {
                return true;
            }
        return false;
    }

    public bool IsInStandingPos()
    {
        return transform.position == (Vector3)guardPointPos;
    }

    public bool IsInAttackPos()
    {
        if(targetEnemy != null) return transform.position == (Vector3)targetPosition;
        else return false;
    }

    public override void MoveProcess()
    {
        transform.position = Vector2.MoveTowards(transform.position, guardPointPos, speed *Time.deltaTime);
    }

    public void GetGuardPoints()
    {
        for(int i = 0; i < guardPointChild.childCount; i++)
        {
            if(index == i) guardPointPos = guardPointChild.GetChild(i).position;
        }
        moveToGuardPoint = true;
    }

    void SetTargetEnemy()
    {
        if(enemyInRange.Count == 0) return;
        if(targetEnemy != null && hadTarget == true) return;
        //foreach( var enemy in enemyInRange)
        for (int i = 0; i < enemyInRange.Count; i++)
        {
            if(enemyInRange[i] == null)
            {
                enemyInRange.RemoveAt(i);
                Debug.Log("avoid enemy null succset");
            }
            if (enemyInRange[i].GetComponent<WalkingEnemy>().soldier == null)
            {
                targetEnemy = enemyInRange[i];
                hadTarget = true;
                enemyInRange[i].GetComponent<WalkingEnemy>().soldier = this;
                break;
            } 
        }
        if(targetEnemy == null)
        {
            targetEnemy = enemyInRange[0];
            hadTarget = false;
        }
    }

    void MoveToTargetEnemy()
    {
        if(targetEnemy == null) return;
        Vector2 offset = new Vector2(0, 0);
        if(index == 0) offset = new Vector2(-0.15f, -0.15f);
        if(index == 1) offset = new Vector2(0, 0);
        if(index == 2) offset = new Vector2(-0.15f, 0.15f);
        
        targetPosition = (Vector2)targetEnemy.GetComponent<WalkingEnemy>().fontPoint.position + offset;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed *Time.deltaTime);
    }

    void UnTargetEnemy()
    {
        //animator.SetBool("isAttack", false);
        if(targetEnemy == null) return;
        targetEnemy.GetComponent<WalkingEnemy>().soldier = null;
        targetEnemy = null;
        hadTarget = false;
    }

    void SetSoldierDirect(Vector2 pos)
    {
        if(transform.position.x < pos.x)
        {
            transform.localScale = currentLocalScale * new Vector2(1,1);
        }
        else if(transform.position.x > pos.x)
        {
            transform.localScale = currentLocalScale * new Vector2(-1,1);
        }
    }


    // public override void CheckToAnimationFightingState()
    // {
    //     if(targetPosition == (Vector2)transform.position)
    //     {
    //         animator.SetBool("isAttack", true);
    //     }
    // }

    // Animation event
    public void AttackEnemy()
    {
        if(targetEnemy == null) return;
        AudioManager.Instance.PlaySound(audioSource, soundEffectSO.GetRandomSwordSound());
        targetEnemy.GetComponent<WalkingEnemy>().TakeDamage(damage);
    }

    public override void Die()
    {
        isDead = true;
        DisableSprites();       
        UnTargetEnemy();
        transform.position = transform.parent.position;
        Invoke(nameof(ReActiveSoldier), barrack.currentSpawnRate);
    }

    void ReActiveSoldier()
    {
        isDead = false;
        HpCurrent = HpMax;             
        ResetHpBar();
        EnableSprites(); 
    }

    private void DisableSprites()
    {
        spriteRenderer.enabled = false;
        healthBar.gameObject.SetActive(false);
    }

    private void EnableSprites()
    {
        spriteRenderer.enabled = true;
        healthBar.gameObject.SetActive(true);
    }
}
