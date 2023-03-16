using UnityEngine;
using UnityEngine.UI;


public class Girl : MonoBehaviour
{

    public Dialog[] dialogs;

    public float[] dist;

    public int[] dialogID;

    public bool isAlenaReturn;

    public Transform pl;
    public Transform[] man;
    public Transform camp;

    public bool[] canTalking;

    public GameObject[] PressE;
    public GameObject[] Dialog;
    public GameObject[] Objcts;
    public GameObject[] About;
    public GameObject Quest;

    public Text[] tObjcts;

    private void Update()
    {
        float x = Vector2.Distance(camp.position, pl.position);

        for(int i = 0; i < dist.Length; i++)
        {
            dist[i] = Vector2.Distance(pl.position, man[i].position);

            if(dist[i] <= 2 && !canTalking[i])
            {
                canTalking[i] = true;

                PressE[i].SetActive(true);
            }

            if (dist[i] >= 2 && canTalking[i])
            {
                canTalking[i] = false;

                PressE[i].SetActive(false);
            }

            if(canTalking[i] && Input.GetKeyDown(KeyCode.E))
            {
                Dialog[i].SetActive(true);
            }
        }
        if(dialogs[0].dialogValue == 1)
        {
            Quest.SetActive(true);
        }
        if(dialogs[1].dialogValue == 4)
        {
            About[0].SetActive(false);
            tObjcts[0].color = Color.gray;
            Objcts[1].SetActive(true);
        }
        if(x < 5)
        {
            About[1].SetActive(false);
            tObjcts[1].color = Color.gray;
            Objcts[2].SetActive(true);
        }
    }

    public void StopAlenaTolking(bool Back)
    {
        About[2].SetActive(false);
        tObjcts[2].color = Color.gray;
        Objcts[3].SetActive(true);
    }

}
