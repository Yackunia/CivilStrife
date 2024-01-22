using DialogueEditor;
using UnityEngine;
using Cinemachine;

public class StartDialog : MonoBehaviour
{
    public NPCConversation thisConversation;

    [SerializeField] private bool isRight;
    private bool canStartDialogue;
    private bool canShowHelp;

    [SerializeField] protected GameObject helpObj;
    [SerializeField] private GameObject canvObj;

    [SerializeField] private Transform plFollow;
    [SerializeField] private Transform plStayFollow;

    [SerializeField] private Transform startPosition;

    [SerializeField] private PlayerAttackSistem attacker;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Slovar slovar;

    [SerializeField] private CinemachineVirtualCamera cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canShowHelp = true;
            helpObj.SetActive(canShowHelp);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canShowHelp = false;
            helpObj.SetActive(canShowHelp);
        }
    }

    private void Update()
    {
        CheckDialogStart();
    }

    private void CheckDialogStart()
    {
        if (canShowHelp && !attacker.plAttacking() && !slovar.isOpened && !canStartDialogue) canStartDialogue = true;
        else if (canStartDialogue) canStartDialogue = false;

        if (canStartDialogue && Input.GetKey(KeyCode.E))
        {
            StartDialogue();
        }
    }

    protected virtual void StartDialogue()
    {
        if (movement.isRight == isRight) movement.Flip();

        canStartDialogue = false;
        canShowHelp = false;
        helpObj.SetActive(canShowHelp);

        cam.Follow = plStayFollow;

        canvObj.SetActive(false);
        movement.StopPlayer();
        //movement.dash.DisableDash();
        movement.wall.DisableClimb();
        movement.wall.DisableWall();
        attacker.DisableCombat();
        slovar.enabled = false;

        movement.GetComponent<Transform>().position = startPosition.position;

        ConversationManager.Instance.StartConversation(thisConversation);
    }

    public virtual void EndDialog()
    {
        canShowHelp = true;
        helpObj.SetActive(true);

        canvObj.SetActive(true);
        movement.UnFreezePlayer();
        //movement.dash.EnableDash();
        movement.wall.EnableClimb();
        movement.wall.EnableWall();
        attacker.EnableCombat();
        slovar.enabled = true;

        ConversationManager.Instance.EndConversation();

        cam.Follow = plFollow;
    }
}
