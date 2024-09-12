using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : ParapolBullet
{
    [SerializeField] Animator animator;
    Coroutine moveBulletCoroutine;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        instantiatePoint = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = Trajectory_num;
        CalTrajectory();
        CalBulletSpeedAndAngle();
        moveBulletCoroutine = StartCoroutine(MoveProcess());
    }

    void Update()
    {
        CalBulletSpeedAndAngle();
        CalTrajectory();
        OnReachTargetLastPos();
    }

    public override void OnReachTargetLastPos()
    {
        if(target != null) return;       
        if (IsReachTargetLastPos())
        {   
            StopCoroutine(moveBulletCoroutine);
            animator.SetTrigger("IsHitGround");          
        }
    }

    public override IEnumerator PlaySoundAndDestroyWhenHit()
    {
        yield return AudioManager.Instance.PlaySoundAndWait(audioSource, soundEffectSO.arrowSound);
        Destroy(gameObject);
    }

    // Animation Event
    public void PlaySoundWhenHitGround()
    {
        AudioManager.Instance.PlaySound(audioSource, soundEffectSO.arrowSound);
    }

    // Animation Event
    public void OnDestroy()
    {
        Debug.Log("CheckDestroy");
        Destroy(gameObject);
    }

}
