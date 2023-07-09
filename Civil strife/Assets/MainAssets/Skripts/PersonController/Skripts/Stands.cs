using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stands : MonoBehaviour
{
    [SerializeField] private GameObject stand;

    [Header("Parameters")]
    public bool canUseStand;
    public bool isUsingStand;

    [SerializeField] private bool staminaFull;

    public float stamina;
    public float staminaMax;

    [SerializeField] private float getStaminaWithoutStand;
    [SerializeField] private float getStaminaWithStand;

    [Header("Player")]

    private PlayerInventory invent;
    private PlayerMovement move;
    private PlayerHealth health;
    private PlayerAttackSistem attacker;
    private CameraEffects effects;

    [Header("Stands")]
    [SerializeField] private GameObject[] stands;

    [SerializeField] private Transform parent;
    private void Start()
    {
        invent = GameObject.Find("pl_INVENTOTY").GetComponent<PlayerInventory>();
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        attacker = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttackSistem>();
        effects = GameObject.Find("Camera_PostProcessingVolume").GetComponent<CameraEffects>();

        effects = GameObject.Find("Camera_PostProcessingVolume").GetComponent<CameraEffects>();

    }
    private void Update()
    {
        StandStartTimer();
        CheckStamina();
    }

    private void CheckStamina()
    {
        if (isUsingStand && stamina > 0) stamina -= Time.deltaTime;

        if (stamina <= 0 && isUsingStand)
        {
            StopStand();
        }
    }

    public void StaminaPlus()
    {
        if (stamina < staminaMax)
        {
            if (isUsingStand) stamina += getStaminaWithStand;
            else stamina += getStaminaWithoutStand;
        }
        if (stamina > staminaMax)
        {
            stamina = staminaMax;
        }
    }

    private void StopStand()
    {
        Debug.Log("1_1");
        effects.SetBloom(false);
        Debug.Log("1_2");

        SetPlayerWithoutStand();
        Debug.Log("1_3");

        Destroy(stand);
        Debug.Log("1_4");

        isUsingStand = false;
        Debug.Log("1_5");
    }

    private void StandStartTimer()
    {
        if (Input.GetButtonDown("Stand") && canUseStand && staminaFull && move.canRun && !health.isHearting && !attacker.plAttacking())
        {
            SpawnStand();
        }
        if (Input.GetButtonDown("Stand") && isUsingStand && stamina < staminaMax*3/4)
        {
            StopStand();
        }

        if (!staminaFull && stamina >= staminaMax && !isUsingStand)
        {
            stamina = staminaMax;
            staminaFull = true;
        }
    }

    private void SpawnStand()
    {
        Debug.Log("1");
        var id = invent.standId;
        staminaFull = false;
        Instantiate(stands[id],parent);
        Debug.Log("2");
        stand = GameObject.FindGameObjectWithTag("Stand");
        isUsingStand = true;
        Debug.Log("3");

        SetPlayerWithStand();
        Debug.Log("4");
        CameraEffects();
        Debug.Log("5");
    }

    private void CameraEffects()
    {
        effects.SetBloom(true);
    }

    private void SetPlayerWithStand()
    {
        move.wall.DisableClimb();
        move.wall.DisableWall();
        move.dash.DisableDash();
        attacker.DisableCombat();
    }
    public void SetPlayerWithoutStand()
    {
        move.wall.EnableClimb();
        move.wall.EnableWall();
        move.dash.EnableDash();
        attacker.EnableCombat();
    }
}
