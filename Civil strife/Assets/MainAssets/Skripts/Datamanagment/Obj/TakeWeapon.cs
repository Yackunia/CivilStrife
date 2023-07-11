    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeWeapon : GetObj
{
    protected override void Start()
    {
        base.Start();

        if (inv.weapons[id])
        {
            Destroy(gameObject);
        }
    }
    protected override void TakeThis()
    {
        if (!inv.weapons[id])
        {
            inv.AddWeapon(id);

            Destroy(gameObject);
        }
    }
}
