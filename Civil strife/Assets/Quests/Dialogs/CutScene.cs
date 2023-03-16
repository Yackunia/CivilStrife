using DialogueEditor;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public NPCConversation thisConversation;

    private bool isStartDialogue;

    [SerializeField] private float TimeToStartDialogue;
    private float timer;

    [SerializeField] private GameObject StopMenu;

    [SerializeField] private Animator ivar;
    [SerializeField] private Animator vsevolod;

    private void Update()
    {
        CheckToStartDialog();
        InputCheck();
    }

    private void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPauseMenu();
        }
    }

    public void SetPauseMenu()
    {
        StopMenu.SetActive(!StopMenu.activeSelf);
        if (Time.timeScale == 1) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    private void CheckToStartDialog()
    {
        if (!isStartDialogue) timer += Time.deltaTime;

        if (timer > TimeToStartDialogue && !isStartDialogue)
        {
            isStartDialogue = true;
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        ConversationManager.Instance.StartConversation(thisConversation);
    }

    public void RandomVsevolod()
    {
        int x = Random.Range(0,10);
        if (x == 10)
        {
            vsevolod.Play("vs2");
        }
    }
    public void RandomIvar()
    {
        int x = Random.Range(0, 10);
        if (x == 10)
        {
            ivar.Play("iv2");
        }
    }
}
