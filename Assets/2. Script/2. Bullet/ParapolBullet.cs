using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParapolBullet : BulletBase
{
    //public LineRenderer lineRenderer;
    [HideInInspector] public Vector2 instantiatePoint;
    [HideInInspector] public int Trajectory_num = 50;
    private float initialAngleDeg;
    private float initialAngleRad;
    private float config = 0.1f;
    private float V;
    private float gravity = 9.8f;

    private List<Vector2> trajectoryPoints = new List<Vector2>();
    private float speedY = 0f;

    protected virtual void CalTrajectory()
    {
        if(target == null) return;
        CalInitialAngle();
        calV();
        trajectoryPoints.Clear();
        for(int i = 0; i < Trajectory_num; i++)
        {
            float time = i * config;
            float X = V * Mathf.Cos(initialAngleRad) * time;
            float Y = V * Mathf.Sin(initialAngleRad) * time - 0.5f * gravity * time * time;
            Vector2 pos = instantiatePoint + new Vector2(X, Y);

            //lineRenderer.SetPosition(i, pos);
            if(trajectoryPoints.Count <= i)
            {
                trajectoryPoints.Add(pos);
            }else
            {
                trajectoryPoints[i] = pos;
            }      
        }
    }

    private void CalInitialAngle()
    {
        float X = target.position.x - instantiatePoint.x;
        float Y = target.position.y - instantiatePoint.y;
        Vector2 dir = new Vector2(X, Y).normalized;
        float dotProduct = Vector2.Dot(dir, Vector2.up);

        float angleRad = Mathf.Acos(dotProduct);
        initialAngleDeg = 90 - angleRad * Mathf.Rad2Deg/2;
        initialAngleDeg = Mathf.Clamp(90 - angleRad * Mathf.Rad2Deg / 2, 60, 90);
        initialAngleRad = initialAngleDeg * Mathf.Deg2Rad;
    }

    private void calV()
    {
        float X = target.position.x - instantiatePoint.x;
        float Y = target.position.y - instantiatePoint.y;
        
        if(X < 0)
        {
            initialAngleRad = -Mathf.Abs(initialAngleRad);
            config = -Mathf.Abs(config);
        }
        else
        {
            initialAngleRad = Mathf.Abs(initialAngleRad);
            config = Mathf.Abs(config);
        }

        float v2 = gravity * X * X / ((Mathf.Tan(initialAngleRad) * X - Y) * 2 * Mathf.Cos(initialAngleRad) * Mathf.Cos(initialAngleRad));
        v2 = Mathf.Abs(v2);
        V = Mathf.Sqrt(v2);
    }

    protected virtual void UpdateBulletSpeedAndAngle()
    {
        if (bulletLastPos != null)
        {
            if (bulletLastPos != (Vector2)transform.position)
            {
                CalBulletRotation();
                AdjustSpeed();
            }     
        }
        bulletLastPos = transform.position;
    }

    protected override void CalBulletRotation()
    {
        // Vector2 moveDir = bulletLastPos - (Vector2)transform.position;
        // float tangentAngle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0, tangentAngle - 90), 1f);
        base.CalBulletRotation();
    }

    private void AdjustSpeed()
    {
        if(bulletLastPos.y > transform.position.y)
        {
            speedY = speed * 1.2f; 
        }
        else
        {
            speedY = speed * 0.7f;
        }
    }

    protected virtual IEnumerator MoveInParapolProcess()
    {        
        for (int i = 0; i < trajectoryPoints.Count; i++)
        {  
            Vector2 position = trajectoryPoints[i];
            while ((Vector2)transform.position != position)
            {
                GetTargetLastPos();
                transform.position = Vector2.MoveTowards(transform.position, position, speedY * Time.deltaTime);
                yield return null;
            }
        }
    }
}
