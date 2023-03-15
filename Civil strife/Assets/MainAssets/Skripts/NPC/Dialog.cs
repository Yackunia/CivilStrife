using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] private GameObject[] dialogWords;

    [SerializeField] private int dialogValue;

    [SerializeField] private GameObject Tasks;
    [SerializeField] private GameObject taskPannel;



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

    private void Update()
    {
        if(dialogValue == 4)
        {
            Tasks.SetActive(true);
        }

        if (dialogValue == 7)
        {
            taskPannel.SetActive(false);
        }
    }

}
