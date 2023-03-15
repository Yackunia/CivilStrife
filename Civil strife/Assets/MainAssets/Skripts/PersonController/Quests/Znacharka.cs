using UnityEngine.UI;

using UnityEngine;

public class Znacharka : MonoBehaviour
{
    [SerializeField] private Transform pl;
    [SerializeField] private PlayerAttackSistem at;
    [SerializeField] private CountOfBerries count;


    [SerializeField] private Button readyBut;

    [SerializeField] private GameObject text;
    [SerializeField] private GameObject Dialog;
    [SerializeField] private GameObject about;
    [SerializeField] private GameObject obj;




    [SerializeField] private float dist;

    private bool isDial;
    private void Start()
    {
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        at = GameObject.Find("Attacker").GetComponent<PlayerAttackSistem>();

    }

    private void Update()
    {
        CheckDialog();

        if(count.count == 5)
        {
            CheckBerry();
            about.SetActive(false);
            obj.SetActive(true);
        }
        


        dist = Vector2.Distance(transform.position, pl.position);
    }

    private void CheckDialog()
    {
        if (dist <= 2 && !isDial)
        {
            isDial = true;
            text.SetActive(isDial);
        }
        if (dist > 2 && isDial)
        {
            isDial = false;
            text.SetActive(isDial);
            Exit();
        }

        if (Input.GetKeyDown(KeyCode.E) && isDial)
        {
            DialogStart();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isDial)
        {
            Exit();
        }
    }

    private void DialogStart()
    {
        at.enabled = false;
        //Time.timeScale = 0f;
        Dialog.SetActive(true);
        text.SetActive(false);
        Cursor.visible = true;
    }

    public void Exit()
    {
        at.enabled = true;
        //Time.timeScale = 1f;
        Dialog.SetActive(false);
        Cursor.visible = false;
    }

    public void CheckBerry()
    {
        readyBut.interactable = true;
    }

}
