using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danila : EnemyV
{
    private bool isHilling;

    [SerializeField] private GameObject canv2;

    [SerializeField] private Transform point;

    protected override void Start()
    {
        base.Start();
        StopToThePoint();
    }
    public void PlayerEnemy()
    {
        ReadyToFight();

        gameObject.layer = 19;
        targLayer = targLayer + LayerMask.GetMask("Player");
        //StartFight();
    }

    public void IHaveNoEnemys()
    {
        rbEnemy().velocity = Vector3.zero;
        if (gameObject.layer == 19)
        {
            targLayer = targLayer - LayerMask.GetMask("Player");

            gameObject.layer = 10;

            isHilling = true;

            isIdle = true;

            anEnemy().SetBool("isHill", isHilling);
        }
        else
        {
            gameObject.layer = 10;

            isHilling = true;
            anEnemy().SetBool("isHill", isHilling);

            Invoke("ReadyToFight", 20f);
        }

        StopFight();

        CheckDeadHitBox();

        canv2.SetActive(false);
    }

    public void StopToThePoint()
    {
        isHilling = true;

        isIdle = true;

        anEnemy().SetBool("isIDLE", isHilling);

        canv2.SetActive(false);
    }

    public void StartMoveToThePoint()
    {
        ReadyToFight();
    }

    private void ReadyToFight()
    {
        gameObject.layer = 18;

        ChangeHealth(enMaxHealth());

        isIdle = false;

        isHilling = false;

        anEnemy().SetBool("isHill", isHilling);
        
        canv2.SetActive(true);
    }

    protected override void Update()
    {
        if(!isHilling) base.Update();

        CheckHealthStatus();
    }

    private void CheckHealthStatus()
    {
        if (enHealth() < enMaxHealth() / 4 && !isHilling)
        {
            IHaveNoEnemys();
        }
    }

    protected override void Damage(float damage)
    {
        StopEnemy();
        DizableCombat();
        base.Damage(damage);
    }

    protected override void EndPain()
    {
        base.EndPain();
        UnFreezeEnemy();
        EnableCombat();
    }

    protected override void Patrol()
    {
        if (enDirection() != 1 && transform.position.x < point.position.x - 0.1f && !isHilling && !isIdle)
        {
            Flip();
        }
        else if (enDirection() == 1 && transform.position.x > point.position.x + 0.1f && !isHilling && !isIdle)
        {
            Flip();
        }

        if (Mathf.Abs(transform.position.x - point.position.x) > 0.1f && !isHilling && !isIdle)
        {
            Move(patroolSpeed);
        }
        else if (Mathf.Abs(transform.position.x - point.position.x) < 0.1f)
        {
            StopToThePoint();
        }
    }
}
