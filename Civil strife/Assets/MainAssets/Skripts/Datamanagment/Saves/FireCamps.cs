using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCamps : MonoBehaviour
{
    public bool notSit = true;
    public int campFireID;

    [SerializeField] private GameObject hint;

    private Transform pl;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!notSit && Input.GetKeyDown(KeyCode.E) && rb.velocity.x <= 0.001f && rb.velocity.x >= -0.001f)
        {
            pl.SendMessage("CampSitting", campFireID);
            hint.SetActive(notSit);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hint.SetActive(notSit);
            notSit = false;
            pl = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hint.SetActive(notSit);
            notSit = true;
            pl = null;
        }
    }
}
