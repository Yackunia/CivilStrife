using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOgBody : MonoBehaviour
{
    [SerializeField] private PlayerMovement plMove;

    [SerializeField] private ParticleSystem stParticle;
    [SerializeField] private ParticleSystem bloodyParticle;
    [SerializeField] private GameObject[] bloodPrefs;

    private SpriteRenderer spr;
    [SerializeField] private Sprite scull;

    public int count = 0;

    [Header("Knock")]
    private Rigidbody2D rb;

    [SerializeField] private float knockSpeedX;
    [SerializeField] private float knockSpeedY;

    private bool isHead = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isHead) Instantiate(bloodPrefs[Random.Range(0, bloodPrefs.Length)], transform.position, Quaternion.identity, null);
    }
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(knockSpeedX, knockSpeedY);

        plMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        spr = gameObject.GetComponent<SpriteRenderer>();
        stParticle.Play();
        bloodyParticle.Play();
    }
    private void InstbloodPart()
    {
        count += 1;
        if (isHead) Instantiate(bloodPrefs[Random.Range(0, bloodPrefs.Length)], transform.position, Quaternion.identity, null);

        if (count > 300 && isHead)
        {
            spr.sprite = scull;
            isHead = false;
            Destroy(bloodyParticle);
            transform.localScale = new Vector3(transform.localScale.x * 1.5f, transform.localScale.y * 1.5f, transform.localScale.z);
        }
        if (count > 500)
        {
            Destroy(gameObject);
        }
    }

    private void Damage(float damage)
    {
        stParticle.Play();
        count += ((int)damage*5);
        Instantiate(bloodPrefs[Random.Range(0, bloodPrefs.Length)], transform.position, Quaternion.identity, null);
        if (plMove.isRight) knockSpeedX = Mathf.Abs(knockSpeedX);
        else knockSpeedX = -1 * Mathf.Abs(knockSpeedX);

        rb.velocity = new Vector2(knockSpeedX, knockSpeedY);

        if (count > 300 && isHead)
        {
            spr.sprite = scull;
            isHead = false;
            Destroy(bloodyParticle);
            transform.localScale = new Vector3(transform.localScale.x * 1.5f, transform.localScale.y * 1.5f, transform.localScale.z);
        }
        if (count > 500)
        {
            Destroy(gameObject);
        }
    }
}