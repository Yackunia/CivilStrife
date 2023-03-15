using UnityEngine;
using UnityEngine.UI;


public class Girl : MonoBehaviour
{
    [SerializeField] private PlayerAttackSistem at;

    public Dialog[] dialogs;

    public float[] dist;

    public int[] dialogID;

    public bool isAlenaReturn;

    public Transform pl;
    public Transform[] man;
    public Transform camp;
    public Transform vilage;


    public bool[] canTalking;

    public GameObject[] PressE;
    public GameObject[] Dialog;
    public GameObject[] Objcts;
    public GameObject[] About;
    public GameObject Quest;

    public Text[] tObjcts;

    public bool[] flag;
    private void Update()
    {
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
                Cursor.visible = false;
            }

            if (canTalking[i] && Input.GetKeyDown(KeyCode.E))
            {
                Dialog[i].SetActive(true);
                at.enabled = false;
                Cursor.visible = true;
            }
        }
        if (dialogs[0].dialogValue == 1 && flag[0])
        {
            Quest.SetActive(true);
        }
        if(dialogs[1].dialogValue == 4 && flag[1])
        {
            About[0].SetActive(false);
            tObjcts[0].color = Color.gray;
            Objcts[1].SetActive(true);
        }
        if(x < 5 && flag[2])
        {
            About[1].SetActive(false);
            tObjcts[1].color = Color.gray;
            Objcts[2].SetActive(true);
        }
        if (y < 5)
        {
            About[3].SetActive(false);
            tObjcts[3].color = Color.gray;
            Objcts[4].SetActive(true);
        }
    }

    public void StopAlenaTolking(bool Back)
    {
        About[2].SetActive(false);
        tObjcts[2].color = Color.gray;
        Objcts[3].SetActive(true);

        isAlenaReturn = Back;
        man[2].gameObject.SetActive(true);
    }

}
