using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : ParapolBullet
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    bool isReachTarget = false;
    Coroutine moveBulletCoroutine;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        instantiatePoint = transform.position;
        //lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.positionCount = Trajectory_num;
        CalTrajectory();
        UpdateBulletSpeedAndAngle();
        moveBulletCoroutine = StartCoroutine(MoveInParapolProcess());
    }

    void Update()
    {
        if(isReachTarget) return;
        UpdateBulletSpeedAndAngle();
        CalTrajectory();
        if(IsReachTargetLastPos())
        {
            OnReachTargetLastPos();
            isReachTarget = true;
        }
    }

    protected override void OnReachTargetLastPos()
    {
        StopCoroutine(moveBulletCoroutine);
        base.EnabeTrailRenderer();

        if(target != null)
        {
            StartCoroutine(PlaySoundAndDestroyWhenHit());
            target.GetComponent<Enemy>().TakeDamage(damage);
            spriteRenderer.enabled = false;
        }
        else
        {
            AudioManager.Instance.PlaySound(audioSource, soundEffectSO.arrowSound);
            animator.SetTrigger("IsHitGround");
        }
    }

    private IEnumerator PlaySoundAndDestroyWhenHit()
    {
        yield return AudioManager.Instance.PlaySoundAndWait(audioSource, soundEffectSO.arrowSound);
        Destroy(gameObject);
    }

    // Animation Event
    public void OnDestroy()
    {
        Destroy(gameObject);
    }

}
