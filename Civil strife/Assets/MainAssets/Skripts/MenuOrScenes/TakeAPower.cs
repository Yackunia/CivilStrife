using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAPower : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
<<<<<<< Updated upstream
    private bool isAdded;
    private void Start()
    {
        Invoke("StartAddingComponents", 0.3f);
        
=======
    private void Start()
    {
        Invoke("StartAddingComponents", .1f);

>>>>>>> Stashed changes
    }

    private void StartAddingComponents()
    {
<<<<<<< Updated upstream
        inventory.AddWeapon(7);
        inventory.SelectWeapon(7);
        for (int i = 0; i < inventory.breastPlates.Length; i++)
        {
            if (inventory.breastPlates[i] && i != 1) inventory.RemoveArmor(i);
        }
=======
        inventory.AddArmor(1);
        inventory.AddWeapon(7);
        inventory.SelectWeapon(7);
        inventory.SelectBreast(1);
>>>>>>> Stashed changes

        for (int i = 0; i < inventory.weapons.Length; i++)
        {
            if (inventory.weapons[i] && i != 7) inventory.RemoveWeapon(i);
        }
<<<<<<< Updated upstream
        for (int i = 0; i < inventory.weapons.Length; i++)
        {
            if (inventory.sekWeapons[i] != 0) inventory.RemoveSekWeapon(i, inventory.sekWeapons[i]);
        }
        inventory.AddArmor(1);
        
        inventory.SelectBreast(1);
        inventory.ChangeBody();
        isAdded = true;
        Debug.Log("new persona");
=======

        for (int i = 0; i < inventory.sekWeapons.Length; i++)
        {
            if (inventory.sekWeapons[i] > 0) inventory.RemoveSekWeapon(i, inventory.sekWeapons[i]);
        }

        for (int i = 0; i < inventory.breastPlates.Length; i++)
        {
            if (inventory.breastPlates[i] && i != 1) inventory.RemoveArmor(i);
        }
>>>>>>> Stashed changes
    }
}
