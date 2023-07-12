using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PlayerAttackSistem : MonoBehaviour
{
    [SerializeField] private PlayerMovement move;
    [SerializeField] private PlayerHealth plHealth;
    [SerializeField] private PlayerInventory inv;
    [SerializeField] private Stands stands;
    [SerializeField] private Settings set;


    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private AudioSource kickAudio;
    [SerializeField] private AudioSource block;

    [Header("Combat")]
    public bool isCombat;

    private bool next = false;
    private bool isAttacking;

    private Transform atHitBoxPosition; 

    [SerializeField] private bool isFirstAttack = true;
    [SerializeField] private bool canCombo = true;
    [SerializeField] private bool needToTake = false;

    [SerializeField] private float attackRad;
    [SerializeField] private float attackDamage;

    [SerializeField] private GameObject[] knifes;

    [SerializeField] private Transform sekAtPos;

    [SerializeField] private LayerMask isDamageable;

    [Header("Animation")]

    [SerializeField] private Animator an;

    [SerializeField] private CameraShake cam;

    [SerializeField] private ParticleSystem particle;

    private void Update()
    {
        CheckAttackInput();
    }

    private void CheckAttackInput()
    {
        if (Input.GetKeyDown(set.attackAction) && isCombat && isFirstAttack)
        {
            StartAttack();
        }

        if (Input.GetKeyDown(set.attackAction) && isCombat && !isFirstAttack && !canCombo && isAttacking)
        {
            ResumeAttack();
        }

        if (Input.GetKeyDown(set.attackAction) && isCombat && !isFirstAttack && canCombo && needToTake && isAttacking)
        {
            EndAttack();
        }

        if (Input.GetKeyDown(set.sekondAttackAction) && !isAttacking && isCombat)
        {
            DropKnife();
        }
    }

    private void ResumeAttack()
    {
        canCombo = true;
        an.SetBool("IsCombo", true);
        an.SetBool("next", next);
        next = !next;
        move.canFlip = false;
        
    }
    private void PlayAttackSound()
    {
        kickAudio.Play();
    }
    private void StartCombo()
    {
        canCombo = false;
        move.canFlip = true;
    }
    private void CheckCombo()
    {
        canCombo = true;
        move.canFlip = false;
    }

    private void StartTake()
    {
        needToTake = true;
        canCombo = false;
    }

    private void EndTake()
    {
        needToTake = false;
    }
    private void StartAttack()
    {
        isAttacking = true;
        isFirstAttack = false;
        canCombo = true;

        move.wall.DisableWall();
        move.wall.DisableClimb();
        //move.dash.DisableDash();
        move.SlowPlayer();
        an.SetBool("IsAttack", isAttacking);
    }
    private void DropKnife()
    {
        if (inv.sekWeapons[inv.sekWeaponId] > 0)
        {
            an.SetBool("Dropping_Plates", true);
        }
    }
    private void InstKnife()
    {
        Instantiate(knifes[inv.sekWeaponId], sekAtPos.position, Quaternion.identity);
        inv.RemoveSekWeapon(inv.sekWeaponId, 1);
    }

    private void EndDropping()
    {
        an.SetBool("Dropping_Plates", false);
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjs = Physics2D.OverlapCircleAll(atHitBoxPosition.position, attackRad, isDamageable);

        foreach (Collider2D col in detectedObjs)
        {
            col.transform.SendMessage("Damage", attackDamage * -move.plFront());
            cam.StartShake();
            stands.StaminaPlus();
        }
    }

    private void EndAttack()
    {
        isAttacking = false;
        isFirstAttack = true;
        canCombo = true;
        needToTake = false;
        next = false;

        an.SetBool("IsAttack", isAttacking);
        an.SetBool("IsCombo", false);
        an.SetBool("next", true);

        move.FastPlayer();
        move.wall.EnableClimb();
        move.wall.EnableWall();
        move.dash.EnableDash();
    }

    private void EndPain()
    {
        isAttacking = false;
        isFirstAttack = true;
        canCombo = true;
        needToTake = false;
        next = false;

        an.SetBool("IsCombo", false);
        an.SetBool("next", true);

        an.SetBool("isPain", false);

        Time.timeScale = 1f;

        if (!stands.isUsingStand)
        {
            move.FastPlayer();
        }
        plHealth.isHearting = false;
        move.wall.isFalling = true;

        plHealth.canHurt = true;
    }
    private void LoadRespawn()
    {
        inv.LoadCamp();
    }
    public void DisableCombat()
    {
        isAttacking = false;
        isFirstAttack = true;
        canCombo = true;
        needToTake = false;
        next = false;
        an.SetBool("IsCombo", false);
        an.SetBool("next", true);


        isCombat = false;
        an.SetBool("IsAttack", isAttacking);
        an.SetBool("IsSekAttack", isAttacking);
        an.SetBool("Dropping_Plates", false);
    }
    public void EnableCombat()
    {
        isCombat = true;
        move.FastPlayer();
    }

    public bool plAttacking()
    {
        return isAttacking;
    }

    #region SetParametersPublic
    public void SetWeapon(float damage, float radius, Transform atCheck)
    {
        attackDamage = damage;
        attackRad = radius;
        atHitBoxPosition = atCheck;
    }
    #endregion

    private void OnDrawGizmos()
    {
       // Gizmos.DrawWireSphere(atHitBoxPosition.position, attackRad);
    }
}
