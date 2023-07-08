using UnityEngine;

public class EnemyV : MonoBehaviour
{
    [Header("PlayerSkripts")]
    private PlayerMovement plMove;

    [Header("Patrool / Chase")]
    //variables needed to implement the Movement of the enemy

    private bool canSeeAnotherEnemy;
    private bool canSeePlayer;
    private bool seePlayer;
    private bool canRun = true;
<<<<<<< Updated upstream
    private bool canFlip = true;

    [SerializeField] private int currentDirection = 1; // временно сериализую для дебага случая с нулевым enDirection()
=======
    [SerializeField] private bool canFlip = true; //временно сериализую для дэша босса

    private int currentDirection = 1; 
>>>>>>> Stashed changes

    private float spawnPoint;

    private string playerTag = "Player";

    private Transform pl;

    private Rigidbody2D rb;

    [SerializeField] private bool isIdle;
    [SerializeField] private bool isRight;

    [SerializeField] private float chaseSpeed;
    [SerializeField] private float patroolSpeed;

    [SerializeField] private Transform drawObject;

    [SerializeField] protected LayerMask anotherEnemys;
<<<<<<< Updated upstream
    
=======

    [Header("Jump")]

    [SerializeField] private bool isWall;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool isJumping;

    [SerializeField] private bool isJumper;

    [SerializeField] private float wallDistance;
    [SerializeField] private float jumpHeight;

    [SerializeField] private Transform wallCheck;

    [SerializeField] private LayerMask whichWall;

    [Header("Collision")]
    private bool isGround;

    [SerializeField] private float grRad;//радиус groundCheck

    [SerializeField] private Transform groundCheck;

>>>>>>> Stashed changes
    [Header("Attack System")]
    //variables of attack system

    private bool canHurtPlayer;
    private bool canHurtObj;    
    private bool canAttack = true;
    private bool isAttacking;

    [SerializeField] private int damage;

    [SerializeField] private float attackRad;
    [SerializeField] private float attackDistance;

    [SerializeField] private Transform attackHitBoxPos;

    [SerializeField] protected LayerMask otherObjLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask destrObjLayer;

    [Header("Raycusts")]
    //variables required for raycasts to work

    [SerializeField] private float plSeeDistance;
    [SerializeField] private float plSeeDistanceBack;
    [SerializeField] private float patroolDistance;

    [SerializeField] private Transform plCheck;
    [SerializeField] private Transform enemysCheck;


    [Header("Animator")]
    private Animator an;

    [Header("Health System")]

    private bool isHearting;
    private bool isAlive = true;

    private float healthMax;

    [SerializeField] private float health;
    [SerializeField] private float knockSpeedX;
    [SerializeField] private float knockSpeedY;

    [SerializeField] private GameObject canv;

    [SerializeField] private Transform healthFieldDrawable;

    private HitZone hitZone;

    [Header("Effects and Audio")]
    [SerializeField] private GameObject money;
    [SerializeField] private GameObject[] bloodPrefs;

    [SerializeField] private ParticleSystem particle;

    [SerializeField] private AudioSource painAud;
    [SerializeField] private AudioSource deathAud;
    [SerializeField] private AudioSource kickAud;
    [SerializeField] private AudioSource walkAudio;

    [Header("Saves")]
    private int idInScene;

    [SerializeField] private bool isRespawn;

    [SerializeField] private SceneData sceneData;


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

    private void SetEnemySimpleParameters()
    {
        spawnPoint = transform.position.x;
        healthMax = health;
    }

    private void SetEnemyModificators()
    {
        hitZone = gameObject.GetComponent<HitZone>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        an = gameObject.GetComponent<Animator>();

        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
    }

    private void SetPlayer()
    {
        pl = GameObject.FindGameObjectWithTag(playerTag).transform;
        plMove = pl.GetComponent<PlayerMovement>();
    }

    #endregion

    #region Status Updating
    private void CheckWhatToDo()
    {
        CheckIsSeePlayer();
        if (!seePlayer) Patrol();
        else ChasePlayer();

        CheckToAttack();

        if (!isAlive) rb.velocity = new Vector2(0f, rb.velocity.y);

<<<<<<< Updated upstream
=======
        CheckToJump();
        Jumping();
>>>>>>> Stashed changes
    }
    private void CheckIsSeePlayer()
    {
        if (!seePlayer && canSeePlayer)
        {
            seePlayer = true;
        }
    }
    protected virtual void UpdAnim()
    {
        an.SetBool("isDead",!isAlive);
        an.SetBool("isDamage", isHearting);
        an.SetBool("isAttacking", isAttacking);
        
    }
    private void CheckLayerStats()
    {
        canSeeAnotherEnemy = Physics2D.Raycast(enemysCheck.position, transform.right, attackDistance * currentDirection * 0.2f, anotherEnemys);

        canSeePlayer = Physics2D.Raycast(plCheck.position, transform.right, plSeeDistance * currentDirection, playerLayer) && isAlive || Physics2D.Raycast(plCheck.position, transform.right, -plSeeDistance * currentDirection / 2, playerLayer) && isAlive;
        canHurtPlayer = Physics2D.Raycast(plCheck.position,transform.right, attackDistance * currentDirection, playerLayer);
        canHurtObj = Physics2D.Raycast(plCheck.position, transform.right, attackDistance * currentDirection, destrObjLayer);
<<<<<<< Updated upstream
=======

        Invoke("CheckJumpLayer", 1f);
    }

    private void CheckJumpLayer()
    {
        isWall = Physics2D.Raycast(wallCheck.position, transform.right, wallDistance * currentDirection, whichWall);
        isGround = Physics2D.OverlapCircle(groundCheck.position, grRad, whichWall);

>>>>>>> Stashed changes
    }
    #endregion

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
    protected void ChaseFlip()
    {
        var enemyX = transform.position.x;
        var playerX = pl.position.x;
        if ((enemyX < playerX - 1f && currentDirection == -1) || (enemyX > playerX + 1f && currentDirection == 1))
        {
            Flip();
        }
    }   
    protected void Flip()
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

<<<<<<< Updated upstream
=======
    #region Jump

    private void CheckToJump()
    {
        if (isWall && !isAttacking && isAlive && canJump && isJumper && !isHearting && isGround)
        {
            StartJump();
        }
    }

    private void StartJump()
    {
        DizableCombat();

        isGround = false;
        isWall = false;

        canFlip = false;
        isJumping = true;
        canJump = false;

        an.SetBool("Jump", isJumping); 
    }

    private void Jumping()
    {
        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    private void StopJump()
    {
        isJumping = false;
        canJump = true;
        EnableCombat();
        canFlip = true;

        an.SetBool("Jump", isJumping);
    }

    #endregion

>>>>>>> Stashed changes
    #region Attack System
    private void CheckToAttack()
    {
        if (canHurtPlayer && canAttack && !isAttacking || canHurtObj && canAttack && !isAttacking)
        {
            StartAttack();
        }
    }
    protected virtual void StartAttack()
    {
        //rb.velocity = new Vector2(0f, 0f);
        isAttacking = true;
        canFlip = false;
        kickAud.Play();
    }
    private void CheckAttackHitBox()
    {
        if (isAlive)
        {
            Collider2D[] detectedObjs = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attackRad, destrObjLayer + playerLayer + otherObjLayer);
            foreach (Collider2D col in detectedObjs)
            {
                col.transform.SendMessage("Damage", damage * enDirection());
            }
        }   
    }
    protected virtual void EndAttack()
    {
        isAttacking = false;
        canFlip = true;
        an.SetBool("isAttacking", isAttacking);
    }
    #endregion

    #region Health System
    private void UpdHealh()
    {
        healthFieldDrawable.localScale = new Vector3(health / healthMax, 1, 1);
    }
    protected virtual void Damage(float damage)
    {
        DamageCalculation(damage);

        KnockEffect();
        BloodEffect();
        DamageSoundEffect();
    }

    protected void DamageCalculation(float damage)
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
        isAlive = false;
        an.SetBool("isDead", !isAlive);

        canv.SetActive(false);

        rb.velocity = new Vector2(0f, 0f);
        gameObject.layer = 10;

        sceneData.SetObjDisabled(isRespawn, idInScene);

        var x = Instantiate(money, transform.position, Quaternion.identity, null);
        x.GetComponent<Rigidbody2D>().velocity = new Vector2(knockSpeedX / 3, knockSpeedY / 3);

        Destroy(hitZone);
        Destroy(this);
    }

    protected virtual void EndPain()
    {
        isHearting = false;
    }

    #endregion

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

        if (isAlive) 
            rb.velocity = new Vector2(knockSpeedX, knockSpeedY);
    }

    private void DamageSoundEffect()
    {
        painAud.Play();
        if (!isAlive)
        {
            deathAud.Play();
            walkAudio.Stop();
        }
    }
    #endregion

    #region For Set Status of Behavior (PUBLIC)
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

        //rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    protected void DizableCombat()
    {
<<<<<<< Updated upstream
        EndAttack();      
=======
        EndAttack();
        canAttack = false;
>>>>>>> Stashed changes
    }

    protected void EnableCombat()
    {
        canAttack = true;
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

    protected float enHealth()
    {
        return health;
    }

    protected float enMaxHealth()
    {
        return healthMax;
    }

    protected PlayerMovement plMovement()
    {
        return plMove;
    }
    #endregion

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attackRad);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(plCheck.position, new Vector3(plCheck.position.x + plSeeDistance * currentDirection, plCheck.position.y, plCheck.position.z) );
        Gizmos.DrawLine(plCheck.position, new Vector3(plCheck.position.x + - plSeeDistance * currentDirection / 2, plCheck.position.y, plCheck.position.z));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(plCheck.position, new Vector3(plCheck.position.x + attackDistance * currentDirection, plCheck.position.y, plCheck.position.z));        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x - patroolDistance,transform.position.y), 1f);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + patroolDistance, transform.position.y), 1f);

        Gizmos.DrawLine(enemysCheck.position, new Vector3(enemysCheck.position.x + attackDistance *0.6f * currentDirection, enemysCheck.position.y, plCheck.position.z));
<<<<<<< Updated upstream
=======
        Gizmos.color = Color.green;

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallDistance * currentDirection, wallCheck.position.y, wallCheck.position.z));
        Gizmos.DrawWireSphere(groundCheck.position, grRad);
>>>>>>> Stashed changes
    }
}
