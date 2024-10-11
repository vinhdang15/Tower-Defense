using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Soldier : Character
{
    // ======= SOLDIER STATUS =======
    public int index;
    
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
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        currentLocalScale = transform.localScale;
        SetupHpCurrent();
    }
    void Update()
    {
        Move();
    }

    protected override void Move()
    {
        if(base.IsDead())
        {
            UnTargetEnemy();
            return;
        }
        
        if(IsMovingToGuardPoint())
        {
            UnTargetEnemy();
            MoveToGuardPoint();
            SetSoldierDirect(guardPointPos);
        }
        else
        {
            moveToGuardPoint = false;
            SetTargetEnemy();
            MoveToTargetEnemy();
            
            if(targetEnemy != null)
            {
                SetSoldierDirect(targetEnemy.position);
            }          
        }

    }

    private void MoveToGuardPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, guardPointPos, speed *Time.deltaTime);
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

    public bool IsMovingToEnemy()
    {
        if(targetEnemy == null) return false;
        else if(transform.position != (Vector3)targetPosition) return true;
        return false;
    }

    public bool IsInGuardPos()
    {
        return transform.position == (Vector3)guardPointPos;
    }

    public bool IsEnemyInFront()
    {
        if(targetEnemy != null) return transform.position == (Vector3)targetPosition;
        else return false;
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

    // Animation event
    public void AttackEnemy()
    {
        if(targetEnemy == null) return;
        AudioManager.Instance.PlaySound(audioSource, soundEffectSO.GetRandomSwordSound());
        targetEnemy.GetComponent<WalkingEnemy>().TakeDamage(damage);
    }

    // Animation event
    public override void OnDead()
    {
        DisableSprites();
        transform.position = transform.parent.position;
        Invoke(nameof(ReActiveSoldier), barrack.currentSpawnRate);
    }
    
    void ReActiveSoldier()
    {
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
