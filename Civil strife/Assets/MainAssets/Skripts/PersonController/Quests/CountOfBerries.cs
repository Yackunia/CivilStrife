using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountOfBerries : MonoBehaviour
{
    public int count;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            count++;
        }
    }
}
