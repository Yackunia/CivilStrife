using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVDasher : HitZone
{
    [SerializeField] private Traian tr;
    [SerializeField] private float dashingDamage;
    protected override void HitCol(Collision2D collision, float damage)
    {
        if (tr.isRunning)
        {
            base.HitCol(collision, dashingDamage);
            tr.StopRun();
        }
        else
        {
            base.HitCol(collision, damageValue);
        }
    }
}
