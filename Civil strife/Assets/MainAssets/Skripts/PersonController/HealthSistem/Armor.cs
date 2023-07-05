using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    [SerializeField] private PlayerInventory inv;

    private int idOfArmor;
    private int idOfHead;

    public float valueOfArmor;
    public float valueMax;
    [SerializeField] private float[] armorAnnal;
    [SerializeField] private float[] helmetAnnal;

    [SerializeField] private GameObject[] slot;
    [SerializeField] private Transform strip;

    public bool ChangeArmorValue(float damage)
    {
        valueOfArmor -= damage;
        strip.localScale = new Vector3(valueOfArmor/2, 1, 1);

        if (valueOfArmor < 40) slot[2].SetActive(false);
        if (valueOfArmor < 20) slot[1].SetActive(false);

        if (valueOfArmor <= 0)
        {
            slot[0].SetActive(false);
            strip.gameObject.SetActive(false);
            return false;
        }
        else return true;
    }
    public void SetArmorObjParameters()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            slot[i].SetActive(false);
        }
        idOfArmor = inv.breastPlateId;
        idOfHead = inv.helmetId;
        valueOfArmor = armorAnnal[idOfArmor] + helmetAnnal[idOfHead];
        valueMax = valueOfArmor;
        strip.localScale = new Vector3(valueOfArmor/2, 1, 1);
        strip.gameObject.SetActive(true);

        for (int i = 0; i < slot.Length; i++)
        {
            if (valueOfArmor - 20 * i > 0) slot[i].SetActive(true);
        }
    }
    public void SetArmorObjParametersNotRespawn()
    {
        strip.localScale = new Vector3(valueOfArmor/2, 1, 1);

        if (valueOfArmor < 40) slot[2].SetActive(false);
        if (valueOfArmor < 20) slot[1].SetActive(false);

        if (valueOfArmor <= 0)
        {
            slot[0].SetActive(false);
            strip.gameObject.SetActive(false);
        }
    }

}
