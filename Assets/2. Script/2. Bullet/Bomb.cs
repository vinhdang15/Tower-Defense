using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Bomb : ParapolBullet
{
    [SerializeField] Animator animator;
    [SerializeField] float boomRange = 2f;
    [SerializeField] List<Unit> enemiesInRange = new List<Unit>();
    CircleCollider2D circleCollider2D;
    Coroutine moveBulletCoroutine;
    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        SetBoomRange();

        instantiatePoint = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = Trajectory_num;
        CalTrajectory();
        CalBulletSpeedAndAngle();
        moveBulletCoroutine = StartCoroutine(MoveProcess());

        audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(audioSource, soundEffectSO.bomWhistleSound);
    }

    void Update()
    {
        CalTrajectory();
        CalBulletSpeedAndAngle();
        OnReachTargetLastPos();
    }

    void SetBoomRange()
    {
        circleCollider2D.radius = boomRange;
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GroundEnemy"))
        {
            enemiesInRange.Add(other.GetComponent<Unit>());       
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("GroundEnemy"))
        {
            enemiesInRange.Remove(other.GetComponent<Unit>());
        }
    }

    public override void OnReachTargetLastPos()
    {
        if (IsReachTargetLastPos())
        {
            StopCoroutine(moveBulletCoroutine);
            animator.SetTrigger("IsHitGround");
        }
    }

    // Animation Event
    public void TakeDamageInRange()
    {   
        if(enemiesInRange.Count == 0) return; 
        List<Unit> enemies = new List<Unit>(enemiesInRange);
        foreach(Unit enemy in enemies)
        {   
            if(enemy != null) enemy.TakeDamage(damage);
        }
    }

    // Animation Event
    public void PlayExplosionSound()
    {
        AudioManager.Instance.PlaySound(audioSource, soundEffectSO.bomExplosionSound);
    }

    // Animation Event
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
