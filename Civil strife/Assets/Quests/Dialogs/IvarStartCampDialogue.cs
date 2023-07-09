using TMPro;
using UnityEngine;
using DialogueEditor;

public class IvarStartCampDialogue : StartDialog
{
    [Header("Data")]
    //main
    public bool isAlive;

    public int respect;

    //start camp
    public bool wasIvarFirstDialoging;
    public bool wasUsladFirstDialog;



    //Quests
    [Header("Output")]

    [SerializeField] private TMP_Text textName;
    

    private void Start()
    {
        if (wasIvarFirstDialoging) textName.text = "Ивар";
    }

    public void ChangeRespect(int value)
    {
        respect += value;
    }

    public void SetRespect(int value)
    {
        respect = value;
    }
    public void ChangeIfFirstDialogue()
    {
        wasIvarFirstDialoging = true;
    }

    protected override void StartDialogue()
    {
        base.StartDialogue();

        ConversationManager.Instance.SetBool("wasUsladFirstDialog", wasUsladFirstDialog);
        ConversationManager.Instance.SetBool("isFirstDialogue", wasIvarFirstDialoging);
        ConversationManager.Instance.SetInt("respect", respect);
    }
}
