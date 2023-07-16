using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanilaHomeClear : MonoBehaviour
{
    [SerializeField] private SceneData sceneData;

    [SerializeField] private DanilaQuestsDialogue dialogueStart;
    [SerializeField] private DanilaQuestsDialogue dialogueEnd;

    [SerializeField] private Danila danila;

    private bool isDanila;

    private void Start()
    {
        danila.StopToThePoint();

        dialogueStart.StartQuestDialogue();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CheckToEndQuest();
        }
        if (collision.gameObject.layer == 18 && !isDanila)
        {
            isDanila = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 18 && isDanila)
        {
            isDanila = false;
        }
    }

    private void CheckToEndQuest()
    {
        bool flag = true;

        for (int i = 86; i < 95;  i++)
        {
            if (sceneData.withRespawn[i])
            {
                flag = false;
            }
        }

        if (flag && isDanila)
        {
            StartEndDialogue();
        }

        Debug.Log(flag.ToString() + isDanila.ToString());
    }

    private void StartEndDialogue()
    {
        dialogueEnd.StartQuestDialogue();
    }

    public void EndQuest()
    {
        dialogueEnd.StartQuestDialogue();
        sceneData.withoutRespawn[0] = false;
        sceneData.withoutRespawn[1] = true;
        sceneData.Save();
    }
}
