using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuests : MonoBehaviour
{
    [Header("Output")]
    [SerializeField] private GameObject[] questPrefs;
    [SerializeField] private GameObject last;
    [SerializeField] private GameObject mainPannel;

    [SerializeField] private Transform parent;

    [SerializeField] private float len;

    [SerializeField] private bool isOpen;

    private void Start()
    {
        for(int i = 0; i < questPrefs.Length; i++)
        {
            last = Instantiate(questPrefs[i], new Vector2(last.transform.position.x, last.transform.position.y + len),Quaternion.identity,parent) ;
        }
    }

    private void Update()
    {
        CheckQuestsPannel();
    }

    private void CheckQuestsPannel()
    {
        if (Input.GetKeyDown(KeyCode.P) && isOpen)
        {
            Time.timeScale = 0f;
            mainPannel.SetActive(isOpen);
        }

        else if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 1f;
            mainPannel.SetActive(isOpen);
        }
    }
}
