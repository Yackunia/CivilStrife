using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] private GameObject[] dialogWords;

    public int dialogValue;




    public void NewReplika(int i)
    {
        dialogWords[dialogValue].SetActive(false);
        dialogValue = i;
        dialogWords[dialogValue].SetActive(true);
    }

    public void NewReplica()
    {
        dialogWords[dialogValue].SetActive(false);
        dialogValue++;
        dialogWords[dialogValue].SetActive(true);
    }
}
