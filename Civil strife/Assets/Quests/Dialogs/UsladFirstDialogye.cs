using DialogueEditor;
using UnityEngine;

public class UsladFirstDialogye : StartDialog
{
    [Header("Data")]
    //main
    public bool isAlive;

    //start camp
    public bool wasFirstUsladDialogue;
    public bool wasIVARFirstDialog;

    public void ChangeWasUsladFirstDialogue()
    {
        wasFirstUsladDialogue = true;
    }

    protected override void StartDialogue()
    {
        base.StartDialogue();

        ConversationManager.Instance.SetBool("wasFirstUsladDialogue", wasFirstUsladDialogue);
        ConversationManager.Instance.SetBool("wasIVARFirstDialog", wasIVARFirstDialog);
    }

}
