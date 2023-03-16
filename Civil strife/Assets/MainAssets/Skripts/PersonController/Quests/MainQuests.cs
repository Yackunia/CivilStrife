using System;
using UnityEngine;
using UnityEngine.UI;


public class MainQuests : MonoBehaviour
{
    [SerializeField] private Znacharka zn;
    [SerializeField] private Girl gr;
    [SerializeField] private Dialog[] dialogs;


    [Header("Output")]
    [SerializeField] private Text[] taskText;

    [SerializeField] private GameObject questPref;
    [SerializeField] private GameObject mainPannel;

    [SerializeField] private Transform parent;

    [SerializeField] private float len;

    [SerializeField] private bool isOpen;

    public int idOfYask;

    [Header("Data")]

    public bool[] isEnabled;
    public int[] idOfCuests;
    public int[] idOfDialog;


    private void Update()
    {
        CheckQuestsPannel();

    }

   

    private void CheckQuestsPannel()
    {
        if (Input.GetKeyDown(KeyCode.P) && isOpen)
        {
            Time.timeScale = 1f;
            isOpen = false;
            mainPannel.SetActive(isOpen);
            Cursor.visible = false;
        }

        else if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0f;
            isOpen = true;
            mainPannel.SetActive(isOpen);
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            questPref.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            questPref.SetActive(false);
        }

        taskText[0].text = taskText[idOfYask].text;
        if (!zn.isQuest && !gr.isQuest) idOfYask = 1;
    }
}
