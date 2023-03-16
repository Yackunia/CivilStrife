using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Girl : MonoBehaviour
{
    [SerializeField] private Inventory2 inv;
    [SerializeField] private PlayerAttackSistem at;
    [SerializeField] private PlayerMovement move;

    [SerializeField] private MainQuests main;


    public Dialog[] dialogs;

    public float[] dist;

    public bool isAlenaReturn;

    public Transform pl;
    public Transform[] man;
    public Transform camp;
    public Transform vilage;


    public bool[] canTalking;
    public bool isQuest;

    public GameObject[] PressE;
    public GameObject[] Dialog;
    public GameObject[] Objcts;
    public GameObject[] About;
    public GameObject Quest;
    public GameObject Pannel;


    public Text[] tObjcts;

    public bool[] flag;

    public bool isStart;
    private void Update()
    {
        CheckOutput();
        Quest.SetActive(isStart);

        float x = Vector2.Distance(camp.position, pl.position);
        float y = Vector2.Distance(vilage.position, pl.position);

        for(int i = 0; i < dist.Length; i++)
        {
            dist[i] = Vector2.Distance(pl.position, man[i].position);

            if (dist[i] <= 2 && !canTalking[i])
            {
                canTalking[i] = true;

                PressE[i].SetActive(true);
            }

            if (dist[i] >= 2 && canTalking[i])
            {
                canTalking[i] = false;

                PressE[i].SetActive(false);
                Dialog[i].SetActive(false);
                at.enabled = true;
                move.enabled = true;
                Cursor.visible = false;
            }

            if (canTalking[i] && Input.GetKeyDown(KeyCode.E))
            {
                PressE[i].SetActive(false);

                Dialog[i].SetActive(true);
                at.enabled = false;
                move.enabled = false;
                Cursor.visible = true;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Dialog[i].SetActive(false);
                at.enabled = true;
                move.enabled = true;
                Cursor.visible = false;
            }
        }
        if (dialogs[0].dialogValue == 1 && flag[0])
        {
            Quest.SetActive(true);
            flag[0] = false;
            flag[1] = true;
            dialogs[1].butsAns[0].interactable = true;
            isStart = true;
        }
        if (dialogs[1].dialogValue == 5 && flag[1])
        {
            About[0].SetActive(false);
            tObjcts[0].color = Color.gray;
            Objcts[1].SetActive(true);
            flag[1] = false;
            flag[2] = true;


        }
        if (x < 5 && flag[2])
        {
            About[1].SetActive(false);
            tObjcts[1].color = Color.gray;
            Objcts[2].SetActive(true);
            flag[2] = false;
            flag[3] = true;
            dialogs[2].butsAns[0].interactable = true;


        }
        if (y < 5 && flag[4])
        {
            About[3].SetActive(false);
            tObjcts[3].color = Color.gray;
            Objcts[4].SetActive(true);
            flag[4] = false;
            flag[5] = true;

        }
        if (dialogs[3].dialogValue == 8 && flag[5] && !isAlenaReturn)
        {
            flag[5] = false;
            Destroy(Quest.GetComponent<Button>());
            inv.AddItem(2, 10);
            Destroy(Pannel);
            isQuest = false;
            flag[6] = true;
        }
        if (dialogs[3].dialogValue == 11 && flag[5] && isAlenaReturn)
        {
            flag[5] = false;
            Destroy(Quest.GetComponent<Button>());
            inv.AddItem(2, 10);
            Destroy(Pannel);
            isQuest = false;
            flag[6] = true;

        }
    }

    private void CheckOutput()
    {
        if (isQuest)
        {
            if (flag[1] == true) main.idOfYask = 4;
            if (flag[2] == true) main.idOfYask = 5;
            if (flag[3] == true) main.idOfYask = 6;
            if (flag[4] == true) main.idOfYask = 7;
            if (flag[5] == true) main.idOfYask = 8;
        }
    }

    public void StopAlenaTolking(bool Back)
    {
        About[2].SetActive(false);
        tObjcts[2].color = Color.gray;
        Objcts[3].SetActive(true);

        isAlenaReturn = Back;
        man[2].gameObject.SetActive(false);
        flag[3] = false;
        flag[4] = true;
        man[3].gameObject.SetActive(true);

        if (Back) dialogs[3].NewReplika(9);
        else dialogs[3].NewReplika(0);

    }

    public void Exit(int id)
    {
        at.enabled = true;
        Cursor.visible = false;
        Dialog[id].SetActive(false);
        move.enabled = true;
    }

    public void SetThisQuest(bool flag)
    {
        isQuest = flag;
    }

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath
   + "/lolipop2.log"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/lolipop2.log", FileMode.Open);
            QuestsInfo2 data = (QuestsInfo2)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < data.flags.Count; i++)
            {
                flag[i] = data.flags[i];

            }
            isQuest = data.isQuest;

            for (int i = 0; i < data.dialogs.Count; i++)
            {
                dialogs[i].dialogValue = data.dialogs[i];
            }
            bool k = data.isStart;
            isStart = k;

            isAlenaReturn = data.isAlenaReturn;

            Debug.Log("Game data loaded!");
        }
        else
            Debug.Log("There is no save data!");
        if (flag[1])
        {
            dialogs[1].butsAns[0].interactable = true;

        }
        if (flag[2])
        {
            About[0].SetActive(false);
            tObjcts[0].color = Color.gray;
            Objcts[1].SetActive(true);
            flag[1] = false;
            flag[2] = true;
        }

        if (flag[3])
        {
            About[0].SetActive(false);
            tObjcts[0].color = Color.gray;
            Objcts[1].SetActive(true);

            About[1].SetActive(false);
            tObjcts[1].color = Color.gray;
            Objcts[2].SetActive(true);

            flag[2] = false;
            flag[3] = true;
            dialogs[2].butsAns[0].interactable = true;

        }

        if (flag[4])
        {
            About[0].SetActive(false);
            tObjcts[0].color = Color.gray;
            Objcts[1].SetActive(true);

            About[1].SetActive(false);
            tObjcts[1].color = Color.gray;
            Objcts[2].SetActive(true);

            About[2].SetActive(false);
            tObjcts[2].color = Color.gray;
            Objcts[3].SetActive(true);

            man[2].gameObject.SetActive(false);
            flag[4] = true;
            man[3].gameObject.SetActive(true);
        }

        if (flag[5])
        {
            About[0].SetActive(false);
            tObjcts[0].color = Color.gray;
            Objcts[1].SetActive(true);

            About[1].SetActive(false);
            tObjcts[1].color = Color.gray;
            Objcts[2].SetActive(true);

            About[2].SetActive(false);
            tObjcts[2].color = Color.gray;
            Objcts[3].SetActive(true);

            About[3].SetActive(false);
            tObjcts[3].color = Color.gray;
            Objcts[4].SetActive(true);

            flag[4] = false;
            flag[5] = true;
            man[2].gameObject.SetActive(false);
            man[3].gameObject.SetActive(true);
        }

        if (flag[6])
        {
            About[0].SetActive(false);
            tObjcts[0].color = Color.gray;
            Objcts[1].SetActive(true);

            About[1].SetActive(false);
            tObjcts[1].color = Color.gray;
            Objcts[2].SetActive(true);

            About[2].SetActive(false);
            tObjcts[2].color = Color.gray;
            Objcts[3].SetActive(true);

            flag[5] = false;
            Destroy(Quest.GetComponent<Button>());
            Destroy(Pannel);
            isQuest = false;
            man[2].gameObject.SetActive(false);
            flag[4] = true;
            man[3].gameObject.SetActive(true);
        }
    }
}
