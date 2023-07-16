using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanilaQuestsDialogue : StartDialog
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private SceneData sceneData;

    protected override void StartDialogue()
    {
        base.StartDialogue();
        helpObj.SetActive(false);
    }

    public override void EndDialog()
    {
        base.EndDialog();
        Destroy(this);
        Destroy(helpObj);
    }
    public void AddSekWeapon()
    {
        if (playerInventory.sekWeapons[1] < 10)
        {
            playerInventory.AddSekWeapon(1, 10 - playerInventory.sekWeapons[1]);

        }
    }

    public void StartQuestDialogue()
    {
        StartDialogue();
    }
}
