using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private PlayerAttackSistem attacker;
    [Header("Archive")]
    //variable for weapon instance
    [SerializeField] private int[] contrArchive;
    [SerializeField] private int[] atCheckersArchive;

    [SerializeField] private float[] damageArchive;
    [SerializeField] private float[] radiusArchive;


    [SerializeField] private Sprite[] spriteArchive;

    [Header("AnimatorControllers")]
    //variable for weapon type
    [SerializeField] private Transform[] attackCheckers;

    [SerializeField] private SpriteRenderer[] weaponObj;
    [SerializeField] private SpriteRenderer[] weaponSekondObj;

    [SerializeField] private RuntimeAnimatorController[] contr;

    [SerializeField] private Animator an;
    public void SetweaponSprite(int id)
    {
        SetTypeOfWeapon(id);
        SetWeaponInstance(id);
    }

    private void SetWeaponInstance(int id)
    {
        weaponObj[contrArchive[id]].sprite = spriteArchive[id];
        weaponSekondObj[contrArchive[id]].sprite = spriteArchive[id];
        attacker.SetWeapon(damageArchive[id], radiusArchive[id], attackCheckers[atCheckersArchive[id]]);
    }

    private void SetTypeOfWeapon(int id)
    {
        an.runtimeAnimatorController = contr[contrArchive[id]];

        for (int i = 0; i < weaponObj.Length; i++)
        {
            weaponObj[i].gameObject.SetActive(false);
        }
        weaponObj[contrArchive[id]].gameObject.SetActive(true);
        Debug.Log(weaponObj[contrArchive[id]].gameObject.name);
    }
}
