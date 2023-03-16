using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pehota : EnemyV
{
    protected override void Death()
    {
        base.Death();
    }

    protected override void Damage(float damage)
    {
        StopEnemy();
        DizableCombat();
        base.Damage(damage);    
    }

    protected override void EndPain()
    {
        base.EndPain();
        UnFreezeEnemy();
        EnableCombat();
    }
}
