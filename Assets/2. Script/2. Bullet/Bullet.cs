using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 1f;
    public Transform target;
    public Vector2 targetLastPos;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public virtual IEnumerator Move()
    {
        while (true)
        {
            GetTargetLastPos();
            if (target != null)
            {
                MoveProcess(target.position);
            }
            else 
            {
                MoveProcess(targetLastPos);
                OnReachTargetLastPos();
            }
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

    public virtual bool ReachTargetLastPos()
    {
        if(Vector2.Distance(transform.position, targetLastPos) <= 0.2f) 
        {
            return true;
        }
        else return false;
    }

    public virtual void MoveProcess(Vector2 position)
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

    public virtual void HitTarget()
    {
        if (target != null)
        {
            target.GetComponent<Unit>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public virtual void OnReachTargetLastPos()
    {
        if (ReachTargetLastPos()) Destroy(gameObject);
    }

    // public IEnumerator TitleFadeOut(Image image)
    // {    
        
    //     if (image == null) yield break;
    //     Color color = image.color;
    //     // Calculate the rate of change per frame
    //     float rate = 1 / titleFadeOutDuration;
    //     color = new Color32(100, 100, 100, 255);
    //     while (color.a > 0.0f)
    //     {
    //         color.a -= rate * Time.deltaTime;
    //         // Apply the new color to the sprite renderer
    //         image.color = color;
    //         yield return null;
    //     }     
    //     Destroy(image.gameObject);
    // }
}
    
    
