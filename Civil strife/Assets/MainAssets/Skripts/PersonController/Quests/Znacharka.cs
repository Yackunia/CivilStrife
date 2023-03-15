using UnityEngine.UI;

using UnityEngine;

public class Znacharka : MonoBehaviour
{
    [SerializeField] private Inventory2 inv;
    [SerializeField] private CountOfBerries count;
    [SerializeField] private PlayerAttackSistem at;


    public Dialog[] dialogs;

    public float[] dist;

    public bool isAlenaReturn;

    public Transform pl;
    public Transform[] man;

    public bool[] canTalking;

    public GameObject[] PressE;
    public GameObject[] Dialog;
    public GameObject[] Objcts;
    public GameObject[] About;
    public GameObject Quest;
    public GameObject Pannel;


    public Text[] tObjcts;

    public bool[] flags;

    private void Update()
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
                Dialog[i].SetActive(true);
                at.enabled = false;
                Cursor.visible = true;
            }
        }
        if (dialogs[0].dialogValue == 3 && flags[0])
        {
            Quest.SetActive(true);
            flags[0] = false;
        }
        if (count.count == 5 && flags[1])
        {
            About[0].SetActive(false);
            tObjcts[0].color = Color.gray;
            Objcts[1].SetActive(true);
            dialogs[0].butsAns[0].interactable = true;
            flags[1] = false;

        }
        if (dialogs[0].dialogValue == 7 && flags[2])
        {
            Destroy(Quest.GetComponent<Button>());
            inv.RemoveItem(1, 5);
            inv.AddItem(2, 10);
            inv.AddItem(3, 2);
            Destroy(Pannel);

            flags[2] = false;

        }
    }

}
