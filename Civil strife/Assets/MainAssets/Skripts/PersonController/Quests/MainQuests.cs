using UnityEngine;
using UnityEngine.UI;


public class MainQuests : MonoBehaviour
{
    [SerializeField] private Znacharka zn;
    [SerializeField] private Girl gr;

    [Header("Output")]
    [SerializeField] private Text[] taskText;

    [SerializeField] private GameObject questPref;
    [SerializeField] private GameObject mainPannel;

    [SerializeField] private Transform parent;

    [SerializeField] private float len;

    [SerializeField] private bool isOpen;

    public int idOfYask;
   

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
        }

        else if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0f;
            isOpen = true;
            mainPannel.SetActive(isOpen);
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
