using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountOfBerries : MonoBehaviour
{
    [SerializeField] private Inventory2 inv;

    public int count;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inv.AddItem(1, 2);
            inv.RemoveItem(1, 1);
       }
    }
}
