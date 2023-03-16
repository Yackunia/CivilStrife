using UnityEngine.UI;

using UnityEngine;

public class Znacharka : MonoBehaviour
{
    [SerializeField] private Inventory2 inv;
    [SerializeField] private CountOfBerries count;
    [SerializeField] private PlayerAttackSistem at;
    [SerializeField] private PlayerMovement move;

    [SerializeField] private MainQuests main;



    public Dialog[] dialogs;

    public float[] dist;

    public bool isAlenaReturn;

    public Transform pl;
    public Transform[] man;

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

    private void Update()
    {
        CheckOutput();

<<<<<<< HEAD
        for (int i = 0; i < dist.Length; i++)
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
                Cursor.visible = false;
            }

            if (canTalking[i] && Input.GetKeyDown(KeyCode.E))
            {
                PressE[i].SetActive(false);
                Dialog[i].SetActive(true);
                at.enabled = false;
                Cursor.visible = true;
            }
        }
        if (dialogs[0].dialogValue == 20 && flag[0])
=======
        CheckIsDialog();
        if (dialogs[0].dialogValue == 3 && flag[0])
>>>>>>> facaf4af91b9883ed62c9d999434cf8e3528bb09
        {
            Quest.SetActive(true);
            flag[0] = false;
            main.idOfYask = 2;
            flag[1] = true;
        }
        if (count.count == 26 && flag[1])
        {
            About[0].SetActive(false);
            tObjcts[0].color = Color.gray;
            Objcts[1].SetActive(true);
            dialogs[0].butsAns[0].interactable = true;
            flag[1] = false;
            flag[2] = true;
            main.idOfYask = 3;

        }
        if (dialogs[0].dialogValue == 7 && flag[2])
        {
            Destroy(Quest.GetComponent<Button>());
            inv.RemoveItem(1, 5);
            inv.AddItem(2, 10);
            inv.AddItem(3, 2);
            Destroy(Pannel);

            flag[2] = false;
            main.idOfYask = 1;
            isQuest = false;
        }
    }

    private void CheckIsDialog()
    {
        for (int i = 0; i < dist.Length; i++)
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
                Cursor.visible = false;
            }

            if (canTalking[i] && Input.GetKeyDown(KeyCode.E))
            {
                PressE[i].SetActive(false);
                Dialog[i].SetActive(true);
                at.enabled = false;
                Cursor.visible = true;
            }
        }

    }


    private void CheckOutput()
    {
        if (isQuest)
        {
            if (flag[1] == true) main.idOfYask = 2;
            if (flag[2] == true) main.idOfYask = 3;
        }
    }

    public void Exit(int id)
    {
        at.enabled = true;
        Cursor.visible = false;
        Dialog[id].SetActive(false);
    }

    public void SetThisQuest(bool flag)
    {
        isQuest = flag;
    }
}
