using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWeapon: GetObj
{
    protected override void TakeThis()
    {
        if (inv.weapons[id] && inv.weaponId != id)
        {
            inv.RemoveWeapon(id);

            Destroy(gameObject);
        }
    }
}
