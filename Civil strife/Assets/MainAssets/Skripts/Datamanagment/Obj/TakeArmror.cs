using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeArmror : GetObj
{
    protected override void Start()
    {
        base.Start();
        if (inv.breastPlates[id]) Destroy(gameObject);
    }
    protected override void TakeThis()
    {
        if (!inv.breastPlates[id])
        {
            inv.AddArmor(id);
            Destroy(gameObject);
        }
    }
}
