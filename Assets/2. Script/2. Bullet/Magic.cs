using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Magic : BulletBase
{
    [SerializeField] private float dealDamageTimeAmount = 0.5f;
    MagicAnimation magicAnimation;
    SpriteRenderer sprite;
    void Start()
    {
        magicAnimation = GetComponent<MagicAnimation>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(MoveProcess());
        AudioManager.Instance.PlaySound(audioSource, soundEffectSO.MagicBallWhistleSound);
    }

    protected override void OnReachTargetLastPos()
    {
        AudioManager.Instance.PlaySound(audioSource, soundEffectSO.MagicBallHitSound);
        if(target != null)
        {
            StartTakeDamageCoroutine();
            magicAnimation.HitEnemyState();
        }
        else magicAnimation.HitGroundState();
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

    public void StartTakeDamageCoroutine()
    {
        Enemy targetScript = target.GetComponent<Enemy>();
        targetScript.TakeDamage(damage);      
        if(targetScript.isUnderDamgeEffect == false)
        {
            targetScript.isUnderDamgeEffect = true;
            StartCoroutine(TakeDamage());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Animation Event
    public void HideBulletSprite()
    {
        sprite.enabled = false;
    }

    // Animation Event
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
