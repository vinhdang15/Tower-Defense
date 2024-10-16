using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] int enemyGold;
    public Vector2 currentPos;
    public void SetEnemyDirection()
    {
        if(currentPos == null) return;
        float x = (transform.position.x - currentPos.x);
        if(x < 0) transform.localScale = new(-1,1);
        else if(x < 0) transform.localScale = new(1,1);
        currentPos = transform.position;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EndPoint"))
        {
            GameController.Instance.UpdateLives(-1);
            Destroy(gameObject);
        }
    }

    protected override void Move()
    {

    }

    // Animation event
    public override void OnDead()
    {
        GameController.Instance.AddGold(enemyGold);
        Destroy(gameObject);
    }
}
