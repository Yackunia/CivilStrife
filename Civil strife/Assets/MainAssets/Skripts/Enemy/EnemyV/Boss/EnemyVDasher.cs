using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVDasher : HitZone
{
    [SerializeField] private Traian tr;
    protected override void HitCol(Collision2D collision)
    {
        base.HitCol(collision);

        if (tr.isRunning) tr.StopRun();
    }
}
