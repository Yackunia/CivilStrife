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

    [SerializeField] private float damageValue;

    [SerializeField] private EnemyV enemyV;
    [SerializeField] private EnemyS enemyS;

    private PlayerMovement move;

    private void Start()
    {
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitTrig(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HitCol(collision);
    }

    protected virtual void HitTrig(Collider2D collision)
    {
        if (collision.tag == "Player" && canHurtPlayer)
        {
            if (needToRotatePlayer) collision.transform.SendMessage("Damage", damageValue * -move.plDirection());
            else if (neetToEnemyVRotate) collision.transform.SendMessage("Damage", damageValue * enemyV.enDirection());
            else if (neetToEnemySRotate) collision.transform.SendMessage("Damage", damageValue * enemyS.enDirection());
        }
        if (collision.transform.tag == "EnemyV" && canHurtEnemy && collision.gameObject.layer != gameObject.layer || 
            collision.transform.tag == "EnemyS" && canHurtEnemy && collision.gameObject.layer != gameObject.layer)
        {
            if (needToRotatePlayer) collision.transform.SendMessage("Damage", damageValue * -move.plDirection());
            else if (neetToEnemyVRotate) collision.transform.SendMessage("Damage", damageValue * enemyV.enDirection());
            else if (neetToEnemySRotate) collision.transform.SendMessage("Damage", damageValue * enemyS.enDirection());
        }
    }
    protected virtual void HitCol(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && canHurtPlayer)
        {
            if (needToRotatePlayer) collision.transform.SendMessage("Damage", damageValue * -move.plDirection());
            else if (neetToEnemyVRotate) collision.transform.SendMessage("Damage", damageValue * enemyV.enDirection());
            else if (neetToEnemySRotate) collision.transform.SendMessage("Damage", damageValue * enemyS.enDirection());
        }
        if (collision.transform.tag == "EnemyV" && canHurtEnemy && collision.gameObject.layer != gameObject.layer ||
            collision.transform.tag == "EnemyS" && canHurtEnemy && collision.gameObject.layer != gameObject.layer)
        {
            if (needToRotatePlayer) collision.transform.SendMessage("Damage", damageValue * -move.plDirection());
            else if (neetToEnemyVRotate) collision.transform.SendMessage("Damage", damageValue * enemyV.enDirection());
            else if (neetToEnemySRotate) collision.transform.SendMessage("Damage", damageValue * enemyS.enDirection());
        }
    }
}
