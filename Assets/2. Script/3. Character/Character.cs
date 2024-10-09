using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    // ======= STATUS =======
    public float speed = 2f;
    public float HpMax;
    public float HpCurrent;
    public int damage;
    public bool isUnderDamgeEffect = false;

    // ======= UNIT MOVEMENT =======
    public Transform fontPoint;
    public Vector2 previousPos;
    public Vector2 direction;

    // ======= HEALTH BAR =======
    public HealthBar healthBar;

    // ======= SOUND =======
    [HideInInspector] public AudioSource audioSource;
    public SoundEffectSO soundEffectSO;
    
    public void SetupHpCurrent()
    {
        HpCurrent = HpMax;
    }

    public void ResetHpBar()
    {
        healthBar.Reset();
    }

     public virtual void Move()
    {
        MoveProcess();
    }

    public virtual void MoveProcess()
    {
        gameObject.GetComponent<PathFinder>().FollowPath(speed);
    }
    
    public void TakeDamage(float damage)
    {
        HpCurrent -= damage;
        healthBar.Resize(HpMax, damage);
        if (HpCurrent <= 0)
        {
            healthBar.gameObject.SetActive(false);
        }
    }

    public void DetectMoveDirection(Vector2 currentPos)
    {
        direction = (currentPos - previousPos);
        if(direction != Vector2.zero)
        {
            direction.Normalize();
        }
    }

    public virtual bool IsDead()
    {
        return HpCurrent <= 0;
    }

    public abstract void OnDead();
    
}
