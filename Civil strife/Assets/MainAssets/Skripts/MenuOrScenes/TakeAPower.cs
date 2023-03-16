using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAPower : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
<<<<<<< HEAD
    private void Start()
    {
        Invoke("StartAddingComponents", .1f);

=======
    private bool isAdded;
    private void Start()
    {
        Invoke("StartAddingComponents", 0.3f);
        
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85
    }

    private void StartAddingComponents()
    {
<<<<<<< HEAD
        inventory.AddArmor(1);
        inventory.AddWeapon(7);
        inventory.SelectWeapon(7);
        inventory.SelectBreast(1);
=======
        inventory.AddWeapon(7);
        inventory.SelectWeapon(7);
        for (int i = 0; i < inventory.breastPlates.Length; i++)
        {
            if (inventory.breastPlates[i] && i != 1) inventory.RemoveArmor(i);
        }
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85

        for (int i = 0; i < inventory.weapons.Length; i++)
        {
            if (inventory.weapons[i] && i != 7) inventory.RemoveWeapon(i);
        }
<<<<<<< HEAD

        for (int i = 0; i < inventory.sekWeapons.Length; i++)
        {
            if (inventory.sekWeapons[i] > 0) inventory.RemoveSekWeapon(i, inventory.sekWeapons[i]);
        }

        for (int i = 0; i < inventory.breastPlates.Length; i++)
        {
            if (inventory.breastPlates[i] && i != 1) inventory.RemoveArmor(i);
        }
=======
        for (int i = 0; i < inventory.weapons.Length; i++)
        {
            if (inventory.sekWeapons[i] != 0) inventory.RemoveSekWeapon(i, inventory.sekWeapons[i]);
        }
        inventory.AddArmor(1);
        
        inventory.SelectBreast(1);
        inventory.ChangeBody();
        isAdded = true;
        Debug.Log("new persona");
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85
    }
}
