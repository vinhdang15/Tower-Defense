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

        instantiatePoint = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = Trajectory_num;
        CalTrajectory();
        CalBulletSpeedAndAngle();
        moveBulletCoroutine = StartCoroutine(MoveBullet());
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
        if (ReachTargetLastPos())
        {   
            StopCoroutine(moveBulletCoroutine);
            animator.SetTrigger("IsHitGround");
        }
    }

    // Animation Event
    public void OnDestroy()
    {
        Destroy(gameObject);
    }

}
