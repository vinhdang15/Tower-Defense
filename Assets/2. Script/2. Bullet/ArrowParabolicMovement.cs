using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ArrowParabolicMovement : MonoBehaviour
{
    //[SerializeField] GameObject arrow;
    [SerializeField] Vector2 instantiatePoint;
    [SerializeField] Vector2 lastPosition;
    [SerializeField] Transform targetPoint = null;
    [SerializeField] float Trajectory_num = 50;
    [SerializeField] float angle_Deg = 15;
    float angle_rad;
    [SerializeField] float config = 0.1f;
    [SerializeField] float V = 0f;

    [SerializeField] float gravity = 9.8f;
    
    // Start is called before the first frame update
    void Start()
    {
        instantiatePoint = transform.position;
        //speed = 10f;
        DrawTrajectory();
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        CalArrowAngle();
    }

    // public override void Move()
    // {
    //     MoveProcess(targetPoint.position);
    // }

    // public override void MoveProcess(Vector2 position)
    // {
    //     transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
    // }

    void calAlpha()
    {
        float X = targetPoint.position.x - instantiatePoint.x;
        float Y = targetPoint.position.y - instantiatePoint.y;
        Vector2 dir = new Vector2(X, Y);
        Vector2 normalizedVector = dir.normalized;
        float dotProduct = Vector2.Dot(normalizedVector, Vector2.up);

        float angle_rad_check = Mathf.Acos(dotProduct);
        angle_Deg = 90 - angle_rad_check * Mathf.Rad2Deg /2;
        angle_rad = angle_Deg * Mathf.Deg2Rad;
    }

    void calV()
    {
        float X = targetPoint.position.x - instantiatePoint.x;
        float Y = targetPoint.position.y - instantiatePoint.y;
        
        if(X < 0)
        {
            angle_rad = -Mathf.Abs(angle_rad);
            config = -Mathf.Abs(config);
        }
        else
        {
            angle_rad = Mathf.Abs(angle_rad);
            config = Mathf.Abs(config);
        }

        float v2 = gravity * X * X / ((Mathf.Tan(angle_rad) * X - Y) * 2 * Mathf.Cos(angle_rad) * Mathf.Cos(angle_rad));
        v2 = Mathf.Abs(v2);
        V = Mathf.Sqrt(v2);
    }
    
    public void Move()
    {
        //GameObject arrowClone = Instantiate(arrow, instantiatePoint, Quaternion.identity);
        Vector3 force = Vector3.zero;
        force.x = V * 50 * Mathf.Cos(angle_rad);
        force.y = V * 50 * Mathf.Sin(angle_rad);
        transform.GetComponent<Rigidbody2D>().AddForce(force);
    }

    void OnDrawGizmosSelected()
    {
        calAlpha();         
        calV();
        Gizmos.color = Color.red;
        for(int i = 0; i < Trajectory_num; i++)
        {
            float time = i * config;
            float X = V * Mathf.Cos(angle_rad) * time;
            float Y = V * Mathf.Sin(angle_rad) * time - 0.5f * gravity * time * time;
            Vector2 pos1 = instantiatePoint + new Vector2(X, Y);

            time = (i + 1)*config;
            X = V * Mathf.Cos(angle_rad) * time;
            Y = V * Mathf.Sin(angle_rad) * time - 0.5f * gravity * time * time;
            Vector2 pos2 = instantiatePoint + new Vector2(X, Y);
            Gizmos.DrawLine(pos1, pos2);
            Vector2 dirnorm = (pos2 - pos1).normalized;

            //transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(dirnorm.y,dirnorm.x) * Mathf.Rad2Deg);
            //Debug.Log(Mathf.Atan2(dirnorm.y,dirnorm.x) * Mathf.Rad2Deg);
        }
    }
    void DrawTrajectory()
    {
        calAlpha();
        calV();

        for(int i = 0; i < Trajectory_num; i++)
        {
            float time = i * config;
            float X = V * Mathf.Cos(angle_rad) * time;
            float Y = V * Mathf.Sin(angle_rad) * time - 0.5f * gravity * time * time;
            Vector2 pos1 = instantiatePoint + new Vector2(X, Y);

            time = (i + 1)*config;
            X = V * Mathf.Cos(angle_rad) * time;
            Y = V * Mathf.Sin(angle_rad) * time - 0.5f * gravity * time * time;
            Vector2 pos2 = instantiatePoint + new Vector2(X, Y);
        }
    }

    
    void CalArrowAngle()
    {
        if (lastPosition != null)
        {
            if (lastPosition == (Vector2)transform.position)
            {
                return;
            }
            Vector2 dir = lastPosition - (Vector2)transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 180);
        }
        lastPosition = transform.position;
    }
}
