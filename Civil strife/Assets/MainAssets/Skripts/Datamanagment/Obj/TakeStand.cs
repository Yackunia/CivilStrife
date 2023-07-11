using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeStand : GetObj
{
    [SerializeField] private Stands stands;
    [SerializeField] private GameObject StandVizual;

    [SerializeField] private ParticleSystem particle;

    
    protected override void Start()
    {
        base.Start();

        stands = GameObject.Find("StandPos").GetComponent<Stands>();

        if (inv.stands[0] && id == 0)
        {
            Destroy(gameObject);
            particle.loop = false;
        }
    }
    protected override void TakeThis()
    {
        if (!inv.stands[id] && inv.standId == 0 && stands.isUsingStand)
        {
            StandVizual.SetActive(true);
        }
        if (id == 0 && !inv.stands[id])
        {
            inv.AddStand(id);
            inv.SelectStand(id);
            Destroy(gameObject);
            particle.loop = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CheckInput();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StandVizual.SetActive(false);
    }

    private void CheckInput()
    {
        if (!inv.stands[id] && inv.standId == 0 && stands.isUsingStand && Input.GetKeyDown(KeyCode.E))
        {
            inv.AddStand(id);
            Destroy(this);
            Destroy(StandVizual);
            particle.loop = false;
        }
        if (!stands.isUsingStand)
        {
            StandVizual.SetActive(false);
        }
    }
}
