using UnityEngine;

public class EnemyS : MonoBehaviour
{
    [Header("PlayerSkripts")]
    private PlayerMovement plMove;

    [Header("Partool / Chase")]
    //variables needed to implement the Movement of the enemy

    private bool canSeeAnotherEnemy;
    private bool seePlayer;
    private bool canSeePlayer;
    private bool canRun = true;
    private bool canFlip = true;

    private int currentDirection = 1;

    private float spawnPoint;

    private string playerTag = "Player";

    private Transform pl;

    private Rigidbody2D rb;

    [SerializeField] private bool isIdle;
    [SerializeField] private bool isRight;

    [SerializeField] private float chaseSpeed;
    [SerializeField] private float patroolSpeed;

    [SerializeField] private Transform drawObject;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask anotherEnemys;

    [Header("Raycusts")]
    //variables required for raycasts to work

    [SerializeField] private float plSeeDistance;
    [SerializeField] private float plSeeDistanceBack;
    [SerializeField] private float patroolDistance;

    [SerializeField] private Transform plCheck;
    [SerializeField] private Transform enemysCheck;

    [Header("Animations")]
    //wow, this is realy Animator!!!!!
    private Animator an;

    [Header("Health System")]
    //parameters for Damage/Hill System

    private bool isHearting;
    private bool isAlive = true;

    private float healthMax;

    [SerializeField] private float health;
    [SerializeField] private float knockSpeedX;
    [SerializeField] private float knockSpeedY;

    [SerializeField] private GameObject canv;
    [SerializeField] private GameObject hitBox;

    [SerializeField] private Transform healthFieldDrawable;

    [Header("Effects and Audio")]
    //Audio, Blood, Drop and other effects

    [SerializeField] private GameObject money;

    [SerializeField] private GameObject[] bloodPrefs;

    [SerializeField] private ParticleSystem particle;

    [SerializeField] private AudioSource painAud;
    [SerializeField] private AudioSource deathAud;

    [SerializeField] private Opening opening;

    [Header("Save")]
    private int idInScene;

    [SerializeField] private SceneData sceneData;

    [SerializeField] private bool isRespawn;


    protected virtual void Start()
    {
        SetAllStartParameters();
    }

    protected virtual void Update()
    {
        CheckLayerStats();
        CheckWhatToDo();
        UpdAnim();
        UpdHealh();
    }

    #region Set Default Variables Or Parameters

    private void SetAllStartParameters()
    {
        SetPlayer();
        SetEnemyModificators();
        SetEnemySimpleParameters();
    }

    private void SetPlayer()
    {
        pl = GameObject.FindGameObjectWithTag(playerTag).transform;
        plMove = pl.GetComponent<PlayerMovement>();
    }

    private void SetEnemyModificators()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        an = gameObject.GetComponent<Animator>();
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
    }

    private void SetEnemySimpleParameters()
    {
        spawnPoint = transform.position.x; //set center point for patrool mechanic
        healthMax = health;
    }

    #endregion
    private void CheckWhatToDo()
    {
        CheckIsSeePlayer();
        if (!seePlayer) Patrol();
        else ChasePlayer();

        if (!isAlive) rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    private void CheckIsSeePlayer()
    {
        if (!seePlayer && canSeePlayer)
        {
            seePlayer = true;
            isIdle = false;
            an.SetBool("isIdle", false);
        }
    }

    protected virtual void UpdAnim()
    {
        an.SetBool("isAlive", isAlive);
        an.SetBool("isPain", isHearting);
        an.SetBool("isIdle", isIdle);
    }
    private void CheckLayerStats()
    {
        canSeeAnotherEnemy = Physics2D.Raycast(enemysCheck.position, transform.right, plSeeDistance * currentDirection * 0.1f, anotherEnemys);
        canSeePlayer = Physics2D.Raycast(plCheck.position, transform.right, plSeeDistance * currentDirection, playerLayer) && isAlive || Physics2D.Raycast(plCheck.position, transform.right, -plSeeDistanceBack * currentDirection, playerLayer) && isAlive;
    }

    #region Movement
    //патрулирование
    private void Patrol()
    {
        if (!isIdle)
        {
            Move(patroolSpeed);
            PatroolFlip();
        }
    }
    private void PatroolFlip()
    {
        var x = transform.position.x;
        if ((x < spawnPoint - patroolDistance && currentDirection == -1) || (x > spawnPoint + patroolDistance && currentDirection == 1))
        {
            Flip();
        }
    }
    //преследование
    private void ChasePlayer()
    {
        Move(chaseSpeed);
        ChaseFlip();
    }
    private void ChaseFlip()
    {
        var enemyX = transform.position.x;
        var playerX = pl.position.x;
        if ((enemyX < playerX - 1f && currentDirection == -1) || (enemyX > playerX + 1f && currentDirection == 1))
        {
            Flip();
        }
    }
    private void Flip()
    {
        if (canFlip)
        {
            currentDirection *= -1;
            isRight = !isRight;

            drawObject.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void Move(float speed)
    {
        if (canRun && !canSeeAnotherEnemy) rb.velocity = new Vector2(speed * currentDirection, rb.velocity.y);
        else if (canRun) rb.velocity = new Vector2(0, rb.velocity.y);
    }
    #endregion

    private void UpdHealh()
    {
        healthFieldDrawable.localScale = new Vector3(health / healthMax, 1, 1);
    }
    protected virtual void Damage(float damage)
    {
        StopEnemy();

        DamageCalculation(damage);

        KnockEffect();
        BloodEffect();
        DamageSoundEffect();
    }

    private void DamageCalculation(float damage)
    {
        damage = Mathf.Abs(damage);

        if (health > damage)
        {
            health -= damage;
            isHearting = true;
        }
        else
        {
            health = 0f;
            Death();
        }
    }

    protected virtual void Death()
    {
        hitBox.SetActive(false);

        opening.DropPrize();

        isAlive = false;
        an.SetBool("isAlive", isAlive);

        canv.SetActive(false);

        rb.velocity = new Vector2(0f, rb.velocity.y);
        gameObject.layer = 10;

        sceneData.SetObjDisabled(isRespawn, idInScene);

        var x = Instantiate(money, transform.position, Quaternion.identity, null);
        x.GetComponent<Rigidbody2D>().velocity = new Vector2(knockSpeedX / 3, knockSpeedY / 3);

        Destroy(this);
    }

    #region Effects
    private void BloodEffect()
    {
        particle.Play();
        Instantiate(bloodPrefs[Random.Range(0, bloodPrefs.Length)], plCheck.position, Quaternion.identity, null);
    }

    private void KnockEffect()
    {
        if (plMove.isRight) knockSpeedX = Mathf.Abs(knockSpeedX);
        else knockSpeedX = -1 * Mathf.Abs(knockSpeedX);

        if (isAlive) rb.velocity = new Vector2(knockSpeedX, knockSpeedY);
    }

    private void DamageSoundEffect()
    {
        if (isAlive) painAud.Play();
        else deathAud.Play();
    }
    #endregion

    #region For Set Status of Behavior (PUBLIC)
    public void EndPain()
    {
        isHearting = false;

        UnFreezeEnemy();
    }
    public void SetIdOfSceneObj(int id, bool flag)
    {
        idInScene = id;
        isRespawn = flag;
    }
    public void DamageWithoutDamage()
    {
        StopEnemy();
        DamageCalculation(0);
        KnockEffect();
    }
    #endregion

    #region For Set Status of Behavior (PRIVATE)

    private void StopFight()
    {
        canRun = false;
        rb.velocity = new Vector2(0f, rb.velocity.y);
        isIdle = true;
        an.SetBool("isIdle", true);
        Destroy(this);
    }
    #endregion

    #region For Set Status of Behavior (PROTECTED)
    protected void StopEnemy()
    {
        canRun = false;
        canFlip = false;

        rb.velocity = new Vector2(0f, 0f);
    }

    protected void UnFreezeEnemy()
    {
        canRun = true;
        canFlip = true;

        rb.velocity = new Vector2(0f, rb.velocity.y);
    }


    #endregion

    #region Public Variable's methods
    public float GetHP()
    {
        return health;
    }

    public int enDirection()
    {
        return currentDirection;
    }
    #endregion

    #region Protected Variable's methods
    protected Rigidbody2D rbEnemy()
    {
        return rb;
    }

    protected Animator anEnemy()
    {
        return an;
    }

    protected bool IsSeePlayer()
    {
        return seePlayer;
    }

    protected bool enHearting()
    {
        return isHearting;
    }
    #endregion

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (!isRight)
        {
            Gizmos.DrawLine(plCheck.position, new Vector3(plCheck.position.x + plSeeDistance, plCheck.position.y, plCheck.position.z));
            Gizmos.color = Color.black;
            Gizmos.DrawLine(plCheck.position, new Vector3(plCheck.position.x + -plSeeDistanceBack, plCheck.position.y, plCheck.position.z));
            Gizmos.color = Color.red;
            Gizmos.DrawLine(enemysCheck.position, new Vector3(enemysCheck.position.x + plSeeDistance * 0.1f, enemysCheck.position.y, plCheck.position.z));
        }
        else
        {
            Gizmos.DrawLine(plCheck.position, new Vector3(plCheck.position.x - plSeeDistance, plCheck.position.y, plCheck.position.z));
            Gizmos.color = Color.black;
            Gizmos.DrawLine(plCheck.position, new Vector3(plCheck.position.x + plSeeDistanceBack, plCheck.position.y, plCheck.position.z));
            Gizmos.color = Color.red;
            Gizmos.DrawLine(enemysCheck.position, new Vector3(enemysCheck.position.x - plSeeDistance * 0.1f, enemysCheck.position.y, plCheck.position.z));

        }

        Gizmos.color = Color.yellow;
        if (!isIdle)
        {
            Gizmos.DrawWireSphere(new Vector2(transform.position.x - patroolDistance, transform.position.y), 1f);
            Gizmos.DrawWireSphere(new Vector2(transform.position.x + patroolDistance, transform.position.y), 1f);
        }

    }
}
