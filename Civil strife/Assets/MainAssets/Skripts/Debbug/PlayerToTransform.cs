using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToTransform : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        Invoke("MovePlayer", 1f);
    }
    private void MovePlayer()
    {
        playerTransform.position = transform.position;  
    }
}
