using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Magic : Bullet
{
    [SerializeField] private float dealDamageTimeAmount = 0.5f;
    void Start()
    {
        StartCoroutine(Move());
    }

    public override void HitTarget()
    {
        if (target != null)
        {   
            StartTakeDamageCoroutine();
            
            transform.GetComponent<SpriteRenderer>().enabled = false;
        }
    } 

    IEnumerator TakeDamage()
    {
        int dealDamageCount = 0;
        Enemy targetScript = target.GetComponent<Enemy>();
        targetScript.speed *= 0.5f;
        while(dealDamageCount <= 2)
        {   
            dealDamageCount++;
            yield return new WaitForSeconds(dealDamageTimeAmount);
            if (target != null) targetScript.TakeDamage(damage);
            else break;
        }
        targetScript.speed *= 2f;
        targetScript.isUnderDamgeEffect = false;
        Destroy(gameObject);
    }

    void StartTakeDamageCoroutine()
    {
        Enemy targetScript = target.GetComponent<Enemy>();
        targetScript.TakeDamage(damage);      
        if(targetScript.isUnderDamgeEffect == false)
        {
            targetScript.isUnderDamgeEffect = true;
            StartCoroutine(TakeDamage());
        }else
        {
            Destroy(gameObject);
        }
    }
}
