using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slovar : MonoBehaviour
{
    public bool isSitting;

    private bool needToStopPlayer;
    private bool needToStopWallSlide;
    private bool needToStopCombat;

    [SerializeField] private GameObject[] infObj;

    [SerializeField] private PlayerAttackSistem attacker;
    [SerializeField] private PlayerMovement move;
    [SerializeField] private Settings set;
    
    [Header("Status")]
    public bool isOpened;

    [SerializeField] private bool isMenu;
    [SerializeField] private bool isMap;
    [SerializeField] private bool isSettings;
    [SerializeField] private bool isSlovar;
    [SerializeField] private bool isInventar;

    [SerializeField] private int statusValue = 0;

    [SerializeField] private GameObject[] menuObjts;
    [SerializeField] private GameObject window;
    [Header("Bestiari")]
    [SerializeField] private int idOfWindow;
    [SerializeField] private GameObject[] BestiaryWindows;
    private void Start()
    {
        SetStatus(0);
        isOpened = false;
    }
    public void SetIdOfBestiary(int id)
    {
        idOfWindow = id;
        for (int i = 0; i < BestiaryWindows.Length; i++)
        {
            BestiaryWindows[i].SetActive(false);
        }
        BestiaryWindows[idOfWindow].SetActive(true);
    }
    private void CheckObjStatus()
    {
        menuObjts[0].SetActive(false);
        menuObjts[1].SetActive(false);
        menuObjts[2].SetActive(false);
        menuObjts[3].SetActive(false);
        menuObjts[4].SetActive(false);

        if (isMenu) menuObjts[0].SetActive(true);
        else if (isSlovar) menuObjts[1].SetActive(true);
        else if (isInventar) menuObjts[2].SetActive(true);
        else if (isMap) menuObjts[3].SetActive(true);
        else if (isSettings) menuObjts[4].SetActive(true);
    }
    public void PlusStatus(int value)
    {
        if (statusValue <= menuObjts.Length && statusValue >= 0 && isSitting)
        {
            statusValue += value;
            SetStatus(statusValue);
        } 
    }
    private void SetStatus(int value)
    {
        statusValue = value;

        isMenu = false;
        isMap = false;
        isSettings = false;
        isSlovar = false;
        isInventar = false;

        if (value == 0) isMenu = true;
        else if (value == 1) isSlovar = true;
        else if (value == 2) isInventar = true;
        else if (value == 3) isMap = true;
        else if (value == 4) isSettings = true;


        CheckObjStatus();
    }
    public void SetWindow(bool status)
    {
        if (isOpened != status)
        {
            isOpened = status;
            window.SetActive(status);
            Cursor.visible = status;

            if (status)
            {
                Time.timeScale = 0f;
                if (attacker.isCombat)
                {
                    attacker.DisableCombat();
                    needToStopCombat = true;
                }
                else
                {
                    needToStopCombat = false;
                }
                if (move.canRun)
                {
                    move.StopPlayer();
                    needToStopPlayer = true;
                }
                else
                {
                    needToStopPlayer = false;
                }
               /* if (move.dash.canDoDash)
                {
                    move.dash.DisableDash();
                    needToStopWallSlide = true;
                }
                else
                {
                    needToStopWallSlide = false;
                }*/
            }
            else
            {
                Time.timeScale = 1f;
                if (needToStopCombat) attacker.EnableCombat();
                if (needToStopPlayer) move.UnFreezePlayer();
                //if (needToStopWallSlide) move.dash.EnableDash();

                for (int i = 0; i < infObj.Length; i++)
                {
                    infObj[i].SetActive(false);
                }
            }
        }
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(set.pauseAction))
        {
            SetStatus(0);
            SetWindow(!isOpened);
        }
        if (Input.GetKeyDown(set.inventoryAction))
        {
            SetStatus(2);
            SetWindow(true);
        }
        if (Input.GetKeyDown(set.mapAction))
        {
            SetStatus(3);
            SetWindow(true);
        }
        if (Input.GetKeyDown(set.bestiaryAction))
        {
            SetStatus(1);
            SetWindow(true);
        }
        if (Input.GetKeyDown(set.settingsAction))
        {
            SetStatus(4);
            SetWindow(true);
        }
    }
}
