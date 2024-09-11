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
    public AudioClip bulletSound;
    
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public virtual IEnumerator Move()
    {
        while (true)
        {
            GetTargetLastPos();
            // if (target != null)
            // {
            //     MoveProcess(target.position);
            // }
            // else 
            // {
            //     MoveProcess(targetLastPos);
            //     OnReachTargetLastPos();
            // }
            MoveTowardProcess(targetLastPos);
            yield return null;

            // magic bullet still deal damege after hitting enemy, so can use this code
            // GetTargetLastPos();
            // MoveProcess(targetLastPos);
            // OnReachTargetLastPos();
            // yield return null;
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
            StartCoroutine(HitTarget());
        }
    }

    public virtual IEnumerator HitTarget()
    {
        if (target != null)
        {
            target.GetComponent<Unit>().TakeDamage(damage);
            PlayAudio();
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(bulletSound.length);
            Destroy(gameObject);
        }
    }

    public virtual void OnReachTargetLastPos()
    {
        if (IsReachTargetLastPos()) Destroy(gameObject);
    }

    void PlayAudio()
    {
        audioSource.PlayOneShot(bulletSound);
        audioSource.pitch = Random.Range(0.95f,1.05f);
    }
}
    
    
