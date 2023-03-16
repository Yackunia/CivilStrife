using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasherHitZone : HitZone
{
    [SerializeField] private Scusihy en;

    protected override void HitCol(Collision2D collision)
    {
        base.HitCol(collision);
        if (collision.transform.tag == "Player" && en.enRun())
        {
            en.DamageWithoutDamage();
            en.StopRunUnscheduled();
        }
    }
}
