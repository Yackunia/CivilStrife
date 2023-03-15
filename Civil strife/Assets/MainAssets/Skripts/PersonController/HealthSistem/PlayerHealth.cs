using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PlayerHealth : MonoBehaviour
{
    [Header("PostProcessing")]
    [SerializeField] private PostProcessVolume post;

    private ChromaticAberration chrom;

    [Header("Animation")]
    [SerializeField] private Animator an;

    public bool isHearting;

    [Header("Skripts")]
    [SerializeField] private PlayerAttackSistem attack;
    [SerializeField] private PlayerMovement move;
    [SerializeField] private PlayerInventar invent;


    [Header("Phys")]
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float knockspeedX;
    [SerializeField] private float knockSpeedY;

    [Header("Stats")]
    public int campID;

    public float healthPoint;

    public float maxHP;

    [SerializeField] private bool isSitting;
    [Header("Other")]
    public GameObject Player;

    [SerializeField] private GameObject[] spr;

    [SerializeField] private AudioSource[] painAudio;

    private bool isAlive;

    private void Start()
    {
        post.profile.TryGetSettings(out chrom);
        chrom.active = false;
    }
    private void Update()
    {
        CalmPain();
    }
    
    public void Damage(float damage)
    {
        chrom.active = true;

        if (damage < 0) knockspeedX = Mathf.Abs(knockspeedX) * -1;
        else knockspeedX = Mathf.Abs(knockspeedX);

        rb.AddRelativeForce(new Vector2(0f, knockSpeedY));

        if (healthPoint > Mathf.Abs(damage))
        {
            painAudio[1].Play();

            healthPoint -= Mathf.Abs(damage);

            isHearting = true;
            an.SetBool("isPain", isHearting);
        }
        else
        {
            healthPoint = 0f;
            KillPLayer();
        }
    }

    private void CalmPain()
    {
        if(isHearting) rb.AddRelativeForce(new Vector2(knockspeedX, 0f));

    }


    private void KillPLayer()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);

        move.enabled = false;
        attack.enabled = false;

        isAlive = false;
        an.SetBool("isAlive", isAlive);

        Time.timeScale = 0.5f;

        spr[0].SetActive(false);
        spr[1].SetActive(true);

        Cursor.visible = true;

        painAudio[0].Play();
    }

    private void PlayerHill(float hpPlus)
    {
        if (hpPlus + healthPoint < maxHP) healthPoint += hpPlus;
        else healthPoint = maxHP;
    }

    public void Respawn(Transform tr)
    {
        Instantiate(Player, tr.position, Quaternion.identity);
        Destroy(gameObject);
    }

    #region SaveSistem
    private void CampSitting(int ID)
    {
        if(rb.velocity.x <= 0.001f && rb.velocity.x >= -0.001f)
        {
            campID = ID;
            isSitting = !isSitting;
            an.SetBool("isSitting", isSitting);
            attack.enabled = !isSitting;
            move.enabled = !isSitting;
            if (!isSitting)
            {
            }
            else
            {
                invent.Save();
            }
        }
    }
    #endregion
}
