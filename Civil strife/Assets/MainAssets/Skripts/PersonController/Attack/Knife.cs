using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerMovement pl;
    private PlayerInventory inv;

    [SerializeField] private int id;

    private float front;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        inv = GameObject.Find("pl_INVENTOTY").GetComponent<PlayerInventory>();

        front = -pl.plFront();
        rb.velocity = new Vector2(speed * front, speed/2);
        rb.AddTorque(1000f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.transform.parent = collision.transform;
        //gameObject.transform.localScale = new Vector2(0.2f, 0.2f);

        if (collision.CompareTag("EnemyS") || collision.CompareTag("EnemyV") || collision.CompareTag("Mish"))
        {
            collision.SendMessage("Damage", damage);
            Destroy(gameObject);
            if (collision.CompareTag("Mish")) inv.AddSekWeapon(id, 1);
        }
        if (collision.CompareTag("Player"))
        {
            inv.AddSekWeapon(id, 1);

            Destroy(gameObject);
            Debug.Log("qffq");
        }
    }
}
