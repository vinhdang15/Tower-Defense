using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Magic : Bullet
{
    [SerializeField] private float dealDamageTimeAmount = 0.5f;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(MoveProcess());
        AudioManager.Instance.PlaySound(audioSource, soundEffectSO.MagicBallWhistleSound);
    }

    public override void HitTarget()
    {
        if (target != null)
        {   
            AudioManager.Instance.PlaySound(audioSource, soundEffectSO.MagicBallHitSound);
            StartCoroutine(PlaySoundAndDestroyWhenHit());
            StartTakeDamageCoroutine();
            transform.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // public override IEnumerator PlaySoundAndDestroyWhenHit()
    // {
    //     yield return AudioManager.Instance.PlaySoundAndWait(audioSource, soundEffectSO.MagicBallWhistleSound);
    //     Destroy(gameObject);
    // }

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
