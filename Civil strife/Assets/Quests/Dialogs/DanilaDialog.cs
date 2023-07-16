using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanilaDialog : StartDialog
{
    //[SerializeField] private Danila danila;

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
        sceneData.withoutRespawn[0] = false;
        sceneData.Save();

        Destroy(helpObj);
    }

    public void AddSekWeapon()
    {
        if (playerInventory.sekWeapons[1] < 10)
        {
            playerInventory.AddSekWeapon(1, 10 - playerInventory.sekWeapons[1]);

        }
    }

}
