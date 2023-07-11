using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObj : MonoBehaviour
{
    protected PlayerInventory inv;

    [SerializeField] protected int id;
    [SerializeField] protected int count;

    protected virtual void Start()
    {
        inv = GameObject.Find("pl_INVENTOTY").GetComponent<PlayerInventory>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TakeThis();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            TakeThis();
        }
    }

    protected virtual void TakeThis()
    {

    }
}
