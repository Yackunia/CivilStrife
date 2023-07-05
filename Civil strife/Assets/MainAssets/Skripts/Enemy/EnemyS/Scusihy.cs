using UnityEngine;

public class Scusihy : EnemyS
{
    [Header("Run")]
    private bool isRunning;
    private bool isPreparation;

    private float runTimer;
    private float runRebootTimer;
    private float rebootError;

    [SerializeField] private bool canRunning;


    [SerializeField] private float runSpeed;
    [SerializeField] private float runTime;
    [SerializeField] private float runRebootTime;
    [SerializeField] private float rebootErrorMax;

    protected override void Start()
    {
        base.Start();
        rebootError = runRebootTime;
    }

    protected override void Update()
    {
        base.Update();

        RunReboot();
        Running();
    }

    private void Running()
    {
        if (isRunning)
        {
            rbEnemy().velocity = new Vector2(runSpeed*enDirection(), rbEnemy().velocity.y);

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

        anEnemy().SetBool("Preparation", isPreparation);

        StopEnemy();
    }

    private void StartRun()
    {
        isRunning = true;
        isPreparation = false;
        anEnemy().SetBool("Preparation", isPreparation);
    }

    private void StopRun()
    {
        StopRunUnscheduled();
        StopEnemy();
        UnFreezeEnemy();
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

            if (runRebootTimer + rebootError >= runRebootTime && !enHearting())
            {
                PreparationBeforeRun();
            }
        }
    }

    protected override void Damage(float damage)
    {
        base.Damage(damage);
        StopRunUnscheduled();
    }

    public bool enRun()
    {
        return isRunning;
    }
}
