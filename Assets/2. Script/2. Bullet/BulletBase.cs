using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] protected float speed = 15f;
    public int damage = 1;
    public Transform target;
    [HideInInspector] public Vector2 targetLastPos;
    [HideInInspector] public Vector2 bulletLastPos;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private ParticleSystem particle;
    

    // ======= SOUND =======
    [HideInInspector] public AudioSource audioSource;
    [SerializeField] protected SoundEffectSO soundEffectSO;
    
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    protected virtual IEnumerator MoveTowardProcess()
    {
        while (true)
        {
            if(target != null) GetTargetLastPos();
            if(!IsReachTargetLastPos())
            {
                UpdateBulletAngle();
                MoveToward(targetLastPos);
            }

            if(IsReachTargetLastPos())
            { 
                OnReachTargetLastPos();
                yield break;
            }
            yield return null;        
        }  
    }
    
    protected virtual void GetTargetLastPos()
    {
        if(target != null) targetLastPos = target.position;
    }

    protected virtual bool IsReachTargetLastPos()
    {
        if(Vector2.Distance(transform.position, targetLastPos) <= 0.2f) 
        {
            return true;
        }
        else return false;
    }

    protected abstract void OnReachTargetLastPos();

    private void MoveToward(Vector2 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
    }

    private void UpdateBulletAngle()
    {
        if(bulletLastPos != null)
        {
            if(bulletLastPos != (Vector2)transform.position)
            {
                CalBulletRotation();
            } 
        }
        bulletLastPos = transform.position;
    }

    protected virtual void CalBulletRotation()
    {
        Vector2 moveDir = bulletLastPos - (Vector2)transform.position;
        float tangentAngle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0, tangentAngle + 180), 1f);
    }

    protected virtual void EnabeTrailRenderer()
    {
        if(trailRenderer != null) trailRenderer.gameObject.SetActive(false);
        if(particle != null) particle.gameObject.SetActive(false);
        
    }
}