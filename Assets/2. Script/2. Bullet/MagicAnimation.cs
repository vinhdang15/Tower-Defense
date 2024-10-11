using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAnimation : BulletAnimationBase
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void ShootingState()
    {
        base.ShootingState();
    }
    // For deal damage and cause effects
    public override void HitEnemyState()
    {
        base.HitEnemyState();
    }
    public override void HitGroundState()
    {
       base.HitGroundState();
    }

    // For deal damage but don't cause effects
    public void HitTargetAndDestroyState()
    {
       base.HitGroundState();
    }
}
