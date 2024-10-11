using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : BulletBase
{
    [SerializeField] private float dealDamageTimeAmount = 0.5f;
    private Enemy targetScript;
    private MagicAnimation magicAnimation;
    private SpriteRenderer sprite;

    private void Awake()
    {
        magicAnimation = GetComponent<MagicAnimation>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        GetEnemyScript();
        AudioManager.Instance.PlaySound(audioSource, soundEffectSO.MagicBallWhistleSound);
        StartCoroutine(MoveTowardProcess());
    }

    private void GetEnemyScript()
    {
        if(target != null)
        {
            targetScript = target.GetComponent<Enemy>();
        }
    }

    protected override void OnReachTargetLastPos()
    {
        AudioManager.Instance.PlaySound(audioSource, soundEffectSO.MagicBallHitSound);
        if(target != null)
        {
            StartTakeDamageCoroutine();
        }
        else magicAnimation.HitGroundState();
    }

    private void StartTakeDamageCoroutine()
    {
        targetScript.TakeDamage(damage);      
        if(!targetScript.isUnderDamgeEffect)
        {
            magicAnimation.HitEnemyState();
            targetScript.isUnderDamgeEffect = true;
            StartCoroutine(TakeDamage());
        }
        else
        {
            magicAnimation.HitTargetAndDestroyState();
        }
    }

    private IEnumerator TakeDamage()
    {
        int dealDamageCount = 0;
        targetScript.currentSpeed *= 0.5f;
        while(dealDamageCount <= 2)
        {   
            dealDamageCount++;
            yield return new WaitForSeconds(dealDamageTimeAmount);
            if (target != null) targetScript.TakeDamage(damage);
            else break;
        }
        targetScript.RestCharacterState();
        Destroy(gameObject);
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
