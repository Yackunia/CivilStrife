using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsladAndTraianDialogue : StartDialog
{
    [SerializeField] private Settings set;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartDialogue();
    }
    public void Destroying()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(set.pauseAction))
        {
            EndDialog();
            Destroy(gameObject);
        }
    }
}
