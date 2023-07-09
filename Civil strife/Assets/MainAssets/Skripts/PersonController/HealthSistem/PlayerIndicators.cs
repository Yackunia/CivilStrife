using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndicators : MonoBehaviour
{
    [SerializeField] private Transform healthFieldDrawable;
    [SerializeField] private Transform staminaFieldDrawable;

    private float hp;
    private float maxHP;
    private float stamina;
    private float staminaMax;

    public PlayerHealth playerHealth;
    public PlayerMovement playerMove;
    public Stands stands;


    private void Start()
    {
        maxHP = playerHealth.maxHP;
        staminaMax = stands.staminaMax;
    }

    private void Update()
    {
        hp = playerHealth.healthPoint;
        stamina = stands.stamina;
        DrawIndicators();
    }

    private void DrawIndicators()
    {
        healthFieldDrawable.localScale = new Vector2(hp/maxHP, 1);
        staminaFieldDrawable.localScale = new Vector2(stamina / staminaMax, 1);
    }
}
