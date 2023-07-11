using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeSekWeapon : GetObj
{
    protected override void TakeThis()
    {
        inv.AddSekWeapon(id, count);
        Destroy(gameObject);
    }
}
