using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAPower : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    private bool isAdded;
    private void Start()
    {
        Invoke("StartAddingComponents", 1.0f);
        
    }

    private void StartAddingComponents()
    {
        inventory.RemoveArmor(0);
        inventory.AddArmor(1);
        inventory.SelectBreast(1);
        isAdded = true;
        Debug.Log("fqwfqf");
    }
}
