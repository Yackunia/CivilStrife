using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCamps : MonoBehaviour
{
    public bool notSit = true;
    public int campFireID;

    [SerializeField] private GameObject hint;

    private Transform pl;


    private void Update()
    {
        if (!notSit && Input.GetKeyDown(KeyCode.E))
        {
            pl.SendMessage("CampSitting", campFireID);
            hint.SetActive(notSit);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hint.SetActive(notSit);
            notSit = false;
            pl = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hint.SetActive(notSit);
            notSit = true;
            pl = null;
        }
    }
}
