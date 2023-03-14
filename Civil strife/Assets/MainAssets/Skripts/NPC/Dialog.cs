using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] private GameObject[] dialogCanvas;
    [SerializeField] private GameObject[] dialogWords;

    [SerializeField] private int dialogValue;

    [SerializeField] private PlayerMovement pl;
    [SerializeField] private PlayerAttackSistem at;

    private void Start()
    {
        pl = GameObject.Find("Player").GetComponent<PlayerMovement>();
        at = GameObject.Find("Attacker").GetComponent<PlayerAttackSistem>();

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && (Input.GetButton("Dialog")))
        {
            DialogStart();
        }
    }
    private void DialogStart()
    {
        pl.canRun = false;
        at.isCombat = false;
        dialogCanvas[0].SetActive(true);
        dialogCanvas[1].SetActive(false);
        Cursor.visible = true;
    }
    public void DialogEnd()
    {
        at.isCombat = true;
        pl.canRun = true;
        dialogCanvas[0].SetActive(false);
        dialogCanvas[1].SetActive(true);
        Cursor.visible = false;
    }

    public void NewReplika(int i)
    {
        dialogWords[dialogValue].SetActive(false);
        dialogValue = i;
        dialogWords[dialogValue].SetActive(true);
    }


}
