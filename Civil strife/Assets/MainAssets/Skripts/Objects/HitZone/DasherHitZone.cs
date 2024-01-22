using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasherHitZone : HitZone
{
    [SerializeField] private Scusihy en;

    protected override void HitCol(Collision2D collision, float damage)
    {
        base.HitCol(collision, damageValue);
        if (collision.transform.tag == "Player" && en.enRun())
        {
            en.DamageWithoutDamage();
            en.StopRunUnscheduled();
        }
        if (collision.transform.tag == "EnemyV" && en.enRun() || 
            collision.transform.tag == "EnemyS" && en.enRun())
        {
            en.DamageWithoutDamage();
            en.StopRunUnscheduled();
        }
    }
}
