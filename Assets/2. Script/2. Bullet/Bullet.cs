using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 1f;
    public Transform target;
    [HideInInspector] public Vector2 targetLastPos;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    [HideInInspector] public AudioSource audioSource;
    public SoundEffectSO soundEffectSO;
    
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public virtual IEnumerator MoveProcess()
    {
        while (true)
        {
            GetTargetLastPos();
            MoveTowardProcess(targetLastPos);
            yield return null;
        }   
    }
    
    public virtual void GetTargetLastPos()
    {
        if(target != null) targetLastPos = target.position;
    }

    public virtual bool IsReachTargetLastPos()
    {
        if(Vector2.Distance(transform.position, targetLastPos) <= 0.2f) 
        {
            return true;
        }
        else return false;
    }

    public virtual void MoveTowardProcess(Vector2 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
           HitTarget();
        }
    }

    // public virtual IEnumerator HitTarget()
    // {
    //     if (target != null)
    //     {
    //         target.GetComponent<Unit>().TakeDamage(damage);
    //         PlayAudio();
    //         spriteRenderer.enabled = false;
    //         yield return new WaitForSeconds(bulletSound.length);
    //         Destroy(gameObject);
    //     }
    // }
    
    public virtual void HitTarget()
    {
        if (target != null)
        {
            StartCoroutine(PlaySoundAndDestroyWhenHit());
            target.GetComponent<Unit>().TakeDamage(damage);
            spriteRenderer.enabled = false;
        }
    }

    public virtual void OnReachTargetLastPos()
    {
        if (IsReachTargetLastPos()) Destroy(gameObject);
    }

    public virtual IEnumerator PlaySoundAndDestroyWhenHit()
    {
        yield return null;
    }
}
    
    
