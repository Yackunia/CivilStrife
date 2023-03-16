using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountOfBerries : MonoBehaviour
{
    [SerializeField] private Inventory2 inv;

    public int count;
    public void Plus() 
    { 
            inv.AddItem(1, 1);
            count+=1;

    }
}
