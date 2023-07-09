using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAPower : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    private void Start()
    {
        Invoke("StartAddingComponents", .1f);

    }

    private void StartAddingComponents()
    {
        inventory.AddArmor(1);
        inventory.AddWeapon(7);
        inventory.SelectWeapon(7);
        inventory.SelectBreast(1);

        for (int i = 0; i < inventory.weapons.Length; i++)
        {
            if (inventory.weapons[i] && i != 7) inventory.RemoveWeapon(i);
        }

        for (int i = 0; i < inventory.sekWeapons.Length; i++)
        {
            if (inventory.sekWeapons[i] > 0) inventory.RemoveSekWeapon(i, inventory.sekWeapons[i]);
        }

        for (int i = 0; i < inventory.breastPlates.Length; i++)
        {
            if (inventory.breastPlates[i] && i != 1) inventory.RemoveArmor(i);
        }
    }
}
