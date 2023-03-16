using UnityEngine.UI;

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
    public bool isStart;

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
        Quest.SetActive(isStart);

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
        }

        if (dialogs[0].dialogValue == 20 && flag[0])

        {
            Quest.SetActive(true);
            flag[0] = false;
            main.idOfYask = 2;
            flag[1] = true;
            isStart = true;

        }
        if (count.count == 5 && flag[1])
        {
            About[0].SetActive(false);
            tObjcts[0].color = Color.gray;
            Objcts[1].SetActive(true);
            dialogs[0].butsAns[0].interactable = true;
            flag[1] = false;
            flag[2] = true;
            main.idOfYask = 3;

        }
        if (dialogs[0].dialogValue == 26 && flag[2])
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
        move.enabled = true;
    }

    public void SetThisQuest(bool flag)
    {
        isQuest = flag;
    }
    private void Start()
    {
        if (File.Exists(Application.persistentDataPath
   + "/lolipop1.log"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/lolipop1.log", FileMode.Open);
            QuestsInfo data = (QuestsInfo)bf.Deserialize(file);
            file.Close();

            for(int i = 0; i < data.flags.Count; i++)
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
            
            Debug.Log("Game data loaded!");
        }
        else
            Debug.Log("There is no save data!");
    }
}
