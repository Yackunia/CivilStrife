using UnityEngine;
using UnityEngine.UI;


public class Dialog : MonoBehaviour
{
    [Header("Speaking")]
    [SerializeField] private GameObject[] word;
    [SerializeField] private GameObject bgObj;
    [SerializeField] private GameObject historyObj;

    [SerializeField] private Transform[] speaker;

    [SerializeField] private Button nextReplikaButton;

    [SerializeField] private Text historyText;

    [TextArea(3,3)]
    [SerializeField] private string[] names;
    [SerializeField] private string[] dialogText;
    public int dialogValue;

    [SerializeField] private bool[] isFirstSpeaker;
    [SerializeField] private bool[] isPlayerSpeaker;

    [Header("Start / Finish Dialog")]
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerMovement move;
    [SerializeField] private Slovar slovar;
    [SerializeField] private PlayerAttackSistem attaker;

    [SerializeField] public bool isDialoging;
    private bool canDialog;

    [SerializeField] private float checkLenght;

    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject plCam;

    [SerializeField] private Transform playerDialogTr;
    [SerializeField] private Transform dialogEye;
    [SerializeField] private Transform pl;
    [SerializeField] private Transform drawObj;

    [SerializeField] private LayerMask plLayer;

    private void Start()
    {
        SetDialogStatus(isDialoging);

        for (int i = 0; i < word.Length; i++) word[i].SetActive(false);
        word[dialogValue].SetActive(true);

        if (isFirstSpeaker[dialogValue]) speaker[0].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        else speaker[1].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

        SetDialogText();
    }

    private void Update()
    {
        UpdStatus();
        InputCheck();
    }
    private void SetDialogText()
    {
        for (int i = 0; i < word.Length; i++)
        {
            string text = word[i].GetComponentInChildren<Text>().text;
            dialogText[i] = text;
        }
    }

    #region Speaking

    public void SetReplika(int i)
    {
        word[dialogValue].SetActive(false);
        word[i].SetActive(true);

        GoDownInHistory();

        dialogValue = i;

        OutputIsPlayerSpeaker();
        SetSizeOfSpeaker();
    }

    public void SetHistotyObjEnabled(bool status)
    {
        bgObj.SetActive(!status);
        historyObj.SetActive(status);
    }

    public void NewReplica()
    {
        word[dialogValue].SetActive(false);
        word[dialogValue + 1].SetActive(true);

        GoDownInHistory();

        dialogValue++;

        OutputIsPlayerSpeaker();
        SetSizeOfSpeaker();
    }

    private void OutputIsPlayerSpeaker()
    {
        if (isPlayerSpeaker[dialogValue])
        {
            nextReplikaButton.interactable = false;
        }
        else
        {
            nextReplikaButton.interactable = true;
        }
    }

    private void GoDownInHistory()
    {
        if (isFirstSpeaker[dialogValue])
            historyText.text = historyText.text + name[0] + dialogText[dialogValue];
        else
            historyText.text = historyText.text + name[1] + dialogText[dialogValue];
    }

    private void SetSizeOfSpeaker()
    {
        if (isFirstSpeaker[dialogValue])
        {
            speaker[0].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            speaker[1].transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            speaker[1].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            speaker[0].transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    #endregion

    #region Start / Finish Dialog
    private void UpdStatus()
    {
        canDialog = Physics2D.Raycast(new Vector2 (dialogEye.position.x - checkLenght, dialogEye.position.y), transform.right, checkLenght * 2, plLayer);
    }
    private void InputCheck()
    {
        if (canDialog && !isDialoging && Input.GetKeyDown(KeyCode.E) && !attaker.plAttacking() && !health.isHearting)
        {
            SetDialogStatus(true);
        }
    }

    public void SetDialogStatus(bool status)
    {
        move.canRun = !status;
        move.canFlip = !status;
        attaker.enabled = !status;
        slovar.enabled = !status;
        plCam.SetActive(!status);

        dialogGameObject.SetActive(status);
        isDialoging = status;

        Cursor.visible = status;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(dialogEye.position, new Vector3(dialogEye.position.x + checkLenght, dialogEye.position.y, dialogEye.position.z));
        Gizmos.DrawLine(dialogEye.position, new Vector3(dialogEye.position.x - checkLenght, dialogEye.position.y, dialogEye.position.z));

    }
}
