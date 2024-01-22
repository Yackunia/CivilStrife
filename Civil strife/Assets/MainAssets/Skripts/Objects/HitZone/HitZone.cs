using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    [SerializeField] private bool needToRotatePlayer = true;
    [SerializeField] private bool neetToEnemyVRotate;
    [SerializeField] private bool neetToEnemySRotate;
    [SerializeField] private bool canHurtEnemy;
    public bool canHurtPlayer;

    [SerializeField] protected float damageValue;

    [SerializeField] private EnemyV enemyV;
    [SerializeField] private EnemyS enemyS;

    private PlayerMovement move;

    private void Start()
    {
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitTrig(collision, damageValue);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HitCol(collision, damageValue);
    }

    protected virtual void HitTrig(Collider2D collision, float damage)
    {
        if (collision.tag == "Player" && canHurtPlayer)
        {
            if (needToRotatePlayer) collision.transform.SendMessage("Damage", damage * -move.plDirection());
            else if (neetToEnemyVRotate) collision.transform.SendMessage("Damage", damage * enemyV.enDirection());
            else if (neetToEnemySRotate) collision.transform.SendMessage("Damage", damage * enemyS.enDirection());
        }
        if (collision.transform.tag == "EnemyV" && canHurtEnemy && collision.gameObject.layer != gameObject.layer || 
            collision.transform.tag == "EnemyS" && canHurtEnemy && collision.gameObject.layer != gameObject.layer)
        {
            if (needToRotatePlayer) collision.transform.SendMessage("Damage", damage * -move.plDirection());
            else if (neetToEnemyVRotate) collision.transform.SendMessage("Damage", damage * enemyV.enDirection());
            else if (neetToEnemySRotate) collision.transform.SendMessage("Damage", damage * enemyS.enDirection());
        }
    }
    protected virtual void HitCol(Collision2D collision, float damage)
    {
        if (collision.transform.tag == "Player" && canHurtPlayer)
        {
            if (needToRotatePlayer) collision.transform.SendMessage("Damage", damage * -move.plDirection());
            else if (neetToEnemyVRotate) collision.transform.SendMessage("Damage", damage * enemyV.enDirection());
            else if (neetToEnemySRotate) collision.transform.SendMessage("Damage", damage * enemyS.enDirection());
        }
        if (collision.transform.tag == "EnemyV" && canHurtEnemy && collision.gameObject.layer != gameObject.layer ||
            collision.transform.tag == "EnemyS" && canHurtEnemy && collision.gameObject.layer != gameObject.layer)
        {
            if (needToRotatePlayer) collision.transform.SendMessage("Damage", damage * -move.plDirection());
            else if (neetToEnemyVRotate) collision.transform.SendMessage("Damage", damage * enemyV.enDirection());
            else if (neetToEnemySRotate) collision.transform.SendMessage("Damage", damage * enemyS.enDirection());
        }
    }
}
