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
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private ParticleSystem particle;

    // ======= SOUND =======
    [HideInInspector] public AudioSource audioSource;
    [SerializeField] protected SoundEffectSO soundEffectSO;
    
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    protected virtual IEnumerator MoveProcess()
    {
        while (true)
        {
            if(target != null) GetTargetLastPos();
            if(!IsReachTargetLastPos()) MoveTowardProcess(targetLastPos);

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

    void MoveTowardProcess(Vector2 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
    }

    protected virtual void EnabeTrailRenderer()
    {
        if(trailRenderer != null) trailRenderer.gameObject.SetActive(false);
        if(particle != null) particle.gameObject.SetActive(false);
        
    }
}