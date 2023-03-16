using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traian : EnemyV
{
    private bool isSekondPhase;

    [Header("Run")]
<<<<<<< HEAD
    public bool isRunning;
    private bool isPreparation;
    [SerializeField] private bool isCoolDown; //временно сериализую
=======
    private bool isRunning;
    private bool isPreparation;
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85

    [SerializeField] private float runTimer;
    [SerializeField] private float runRebootTimer;
    [SerializeField] private float rebootError;

    [SerializeField] private bool canRunning;


    [SerializeField] private float runSpeed;
    [SerializeField] private float runTime;
    [SerializeField] private float runRebootTime;
    [SerializeField] private float rebootErrorMax;

    #region Ovveride
<<<<<<< HEAD

=======
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85
    protected override void Start()
    {
        base.Start();
        rebootError = runRebootTime;
    }
    protected override void Update()
    {
        base.Update();
        CheckSekPhase();
        RunReboot();
        Running();
    }

    protected override void UpdAnim()
    {
        base.UpdAnim();
        anEnemy().SetBool("isdashing", isRunning);
    }
<<<<<<< HEAD

    protected override void Damage(float damage)
    {
        if (!isPreparation && !isRunning) base.Damage(damage);
    }

=======
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85
    protected override void StartAttack()
    {
        base.StartAttack();
        StopEnemy();
    }

    protected override void EndAttack()
    {
        base.EndAttack();
        UnFreezeEnemy();
        anEnemy().SetBool("Combo", false);
    }
    #endregion
    private void SekAttack()
    {
<<<<<<< HEAD
        if (isSekondPhase)
        {
            StartSekAttack();
        }

        else
        {
            int x = Random.Range(0, 3);
            if (x == 3)
            {
                StartSekAttack();
            }
        }
    }

    private void StartSekAttack()
    {
        anEnemy().SetBool("Combo", true);
        StopEnemy();
    }
=======
        if (isSekondPhase) anEnemy().SetBool("Combo", true);
        else
        {
            int x = Random.Range(0, 5);
            if (x == 3) anEnemy().SetBool("Combo", true);
        }
    }
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85
    private void CheckSekPhase()
    {
        if (enHealth() < enMaxHealth() / 1.5f && !isSekondPhase)
        {
            StartSekPhase();
        }
    }

    private void StartSekPhase()
    {
        isSekondPhase = true;
        canRunning = true;
        runRebootTimer = runRebootTime;
    }
    private void Running()
    {
        if (isRunning)
        {
            rbEnemy().velocity = new Vector2(runSpeed * enDirection(), rbEnemy().velocity.y);

            runTimer += Time.deltaTime;

            if (runTimer > runTime)
            {
                StopRun();
            }
        }
    }

    private void PreparationBeforeRun()
    {
        isPreparation = true;
        runRebootTimer = 0f;
        anEnemy().SetBool("Combo", false);
        anEnemy().SetBool("Preparation", isPreparation);
        UnFreezeEnemy();
        ChaseFlip();
        DizableCombat();
        StopEnemy();
    }

    private void StartRun()
    {
        isRunning = true;
        isPreparation = false;
        anEnemy().SetBool("Preparation", isPreparation);
        gameObject.layer = 12;
    }

<<<<<<< HEAD
    public void StopRun()
    {
        gameObject.layer = 9;

        StopRunUnscheduled();
        StopEnemy();

        RunCoolDown();
=======
    private void StopRun()
    {
        gameObject.layer = 9;
        StopRunUnscheduled();
        StopEnemy();
        UnFreezeEnemy();
        EnableCombat();
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85
    }
    public void StopRunUnscheduled()
    {
        rebootError = Random.Range(0, rebootErrorMax);
        runTimer = 0f;
        isRunning = false;
        isPreparation = false;
        anEnemy().SetBool("Preparation", isPreparation);
    }

    private void RunReboot()
    {
        if (!isRunning && !isPreparation && canRunning && IsSeePlayer())
        {
            runRebootTimer += Time.deltaTime;

            if (runRebootTimer + rebootError >= runRebootTime)
            {
                PreparationBeforeRun();
            }
        }
    }
<<<<<<< HEAD

    private void RunCoolDown()
    {
        isCoolDown = true;
        anEnemy().SetBool("isdashing", isRunning);
        anEnemy().SetBool("CoolDown", isCoolDown);
    }

    private void StopCoolDown()
    {
        isCoolDown = false;
        UnFreezeEnemy();
        EnableCombat();
        anEnemy().SetBool("CoolDown", isCoolDown);
    }
=======
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85
}
