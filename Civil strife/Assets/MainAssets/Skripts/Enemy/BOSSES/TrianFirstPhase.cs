using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianFirstPhase : Enemy
{

    [SerializeField] private HitZone hitB;

    public bool lol;
    public bool phase = false;

    [SerializeField] private Transform staminaBar;

    [SerializeField] private GameObject cutscene;
    [SerializeField] private GameObject pl;

    private float timerMax;


    protected override void Start()
    {
        base.Start();
        ChangeCanAttack(true);
        timerMax = GetTimeToReboot();
    }


    protected override void Update()
    {
        base.Update();
        lol = GetCanAttack();
        staminaBar.localScale = new Vector2(GetRebootTimer() / timerMax, 1);
    }

    protected override void Damage(float damage)
    {
        particle.Play();

        if (GetHealth() > damage)
        {
            enemAudio[1].Play();

            SetHealth(damage);
        }
        else
        {
            SetHealth(GetHealth());
            Death();
        }
    }

    protected override void EndAttack()
    {
        isAttacking = false;
        enemyAnim.SetBool("isAttacking", false);
        ChangePermissionCanMoveOrNot(true);
        //ChangeCanAttack(true);
    }
    public void StartCutScene()
    {
        pl.SetActive(false);
        cutscene.SetActive(true);
        Destroy(gameObject);

    }
    protected override void Death()
    {
        if (!phase)
        {
            StartNewPhase();
        }
        else
        {
            base.Death();

            gameObject.GetComponent<TrianFirstPhase>().enabled = false;
            Destroy(hitB);
        }
    }

    private void StartNewPhase()
    {
        SetHealth(-80f);
        phase = true;
        ChangeCanDashOrNot(true);
    }
}
