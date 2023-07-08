using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    [SerializeField] private int[] id;
    [SerializeField] private LayerMask pl_Layer;

    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject obj2;

    [SerializeField] private PlayerInventory invent;
    [SerializeField] private PlayerMovement move;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerAttackSistem attacker;
    [SerializeField] private CameraEffects effects;
    [SerializeField] private Slovar slovar;
    [SerializeField] private SceneData scene;


    private bool isPlayer;
    private bool onCamp;

    private void Start()
    {
        invent = GameObject.Find("pl_INVENTOTY").GetComponent<PlayerInventory>();
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        attacker = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttackSistem>();
        effects = GameObject.Find("Camera_PostProcessingVolume").GetComponent<CameraEffects>();
        slovar = GameObject.Find("Cameras_Slovar").GetComponent<Slovar>();
    }
    private void Update()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        if (!onCamp)
        {
            isPlayer = Physics2D.Raycast(transform.position, transform.forward, 2f, pl_Layer) ||
                        Physics2D.Raycast(transform.position, transform.forward, -2f, pl_Layer);
            if (isPlayer)
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.E) && isPlayer && !onCamp)
        {
            attacker.DisableCombat();
            onCamp = true;
            obj.SetActive(false);
            obj2.SetActive(true);
            move.Sitting(true);
            move.dash.DisableDash();
            move.wall.DisableWall();
            health.canHurt = false;
            health.healthPoint = health.maxHP;
            invent.armorData = invent.armor.valueMax;
            invent.SetCamp(id[0], id[1]);
<<<<<<< Updated upstream
=======
            invent.SetInvBut(true);
>>>>>>> Stashed changes
            slovar.isSitting = true;
            scene.Respawn();
        }
        if (Input.GetKeyDown(KeyCode.D) && onCamp && !slovar.isOpened || Input.GetKeyDown(KeyCode.A) && onCamp && !slovar.isOpened)
        {
            onCamp = false;
            effects.StartAnim(false);
            move.Sitting(false);
            slovar.isSitting = false;
            //sceneLoader.LoadScene(id[1]);
        }
    }

    public void Save()
    {
        attacker.DisableCombat();
        onCamp = true;
        obj.SetActive(false);
        obj2.SetActive(true);
        move.Sitting(true);
        move.dash.DisableDash();
        move.wall.DisableWall();
        health.canHurt = false;
        health.healthPoint = health.maxHP;
        invent.armorData = invent.armor.valueMax;
        invent.SetCamp(id[0], id[1]);
        slovar.isSitting = true;

        onCamp = false;
        move.Sitting(false);
        slovar.isSitting = false;
        //sceneLoader.LoadScene(id[1]);
    }
}
