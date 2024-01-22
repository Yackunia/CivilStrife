using Unity.VisualScripting;
using UnityEngine;

public class EnemyV : MonoBehaviour
{
    [Header("Patrool / Chase")]
    //variables needed to implement the Movement of the enemy

     private bool canSeeAnotherEnemy;
     private bool canSeeTarget;
     private bool seeTarget;
    private bool canRun = true;
    private bool canFlip = true; //временно сериализую для дэша босса

    [SerializeField] private int currentDirection = 1; 

    private float spawnPoint;

    private Transform target;

    private Rigidbody2D rb;

    [SerializeField] protected bool isIdle;
    [SerializeField] private bool isRight;

    [SerializeField] private float chaseSpeed;
    [SerializeField] protected float patroolSpeed;
    [SerializeField] private float attackSpeed;


    [SerializeField] private Transform drawObject;

    [SerializeField] protected LayerMask anotherEnemys;

    [Header("Jump")]

    private bool isWall;
    private bool canJump = true;
    private bool isJumping;

    [SerializeField] private bool isJumper;

    [SerializeField] private float wallDistance;
    [SerializeField] private float jumpHeight;

    [SerializeField] private Transform wallCheck;

    [SerializeField] private LayerMask whichWall;

    [Header("Collision")]
    private bool isGround;

    [SerializeField] private float grRad;//радиус groundCheck

    [SerializeField] private Transform groundCheck;

    [Header("Attack System")]
    //variables of attack system

    private bool canHurtTarget;
    private bool canHurtObj;    
    private bool canAttack = true;
    [SerializeField] private bool isAttacking;

    [SerializeField] private int damage;

    [SerializeField] private float attackRad;
    [SerializeField] private float attackDistance;
    private float attackDistanceError;
    [SerializeField] private float attackDistanceErrorMax;


    [SerializeField] private Transform attackHitBoxPos;

    [SerializeField] protected LayerMask otherObjLayer;
    [SerializeField] protected LayerMask targLayer;
    [SerializeField] private LayerMask destrObjLayer;

    [Header("Raycusts")]
    //variables required for raycasts to work

    [SerializeField] private float targetSeeDistance;
    [SerializeField] private float plSeeDistanceBack;
    [SerializeField] private float patroolDistance;

    [SerializeField] private Transform targetsCheck;
    [SerializeField] private Transform otherEnemyCheck;


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
    [SerializeField] private AudioSource HitBoxAud;
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
        SetEnemySimpleParameters();
        SetEnemyModificators();
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


    #endregion

    #region Status Updating
    private void CheckWhatToDo()
    {
        CheckIsSeePlayer();
        if (!seeTarget) Patrol();
        else ChasePlayer();

        CheckToAttack();

        if (!isAlive) rb.velocity = new Vector2(0f, rb.velocity.y);

        CheckToJump();
        Jumping();

        CheckToStopEnemy();
    }

    private void CheckToStopEnemy()
    {
        if (canSeeAnotherEnemy && canRun && !isHearting && !isAttacking && !isJumping && !isWall && isAlive && seeTarget)
        {
            StopEnemy();
        }

        if (canSeeAnotherEnemy || isIdle) 
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (!canSeeAnotherEnemy && !canRun && !isHearting && !isAttacking && !isJumping && !isWall && isAlive)
        {
            Invoke("UnFreezeEnemy", .5f);
        }
    }

    private void CheckIsSeePlayer()
    {
        if (!seeTarget && canSeeTarget)
        {
            StartFight();
        }
    }
    protected void StartFight()
    {
        FindTarget();
        seeTarget = true;
        isIdle = false;
        
    }

    private void FindTarget()
    {
        RaycastHit2D[] raycastHit2Ds = Physics2D.RaycastAll(targetsCheck.position, transform.right, (targetSeeDistance + 1) * currentDirection, targLayer);

        bool flag = true;

        foreach (RaycastHit2D col in raycastHit2Ds)
        {
            if (col.transform != transform)
            {
                target = col.transform;

                flag = false;

                break;
            }
        }

        raycastHit2Ds = Physics2D.RaycastAll(targetsCheck.position, transform.right, -plSeeDistanceBack * currentDirection, targLayer);

        foreach (RaycastHit2D col in raycastHit2Ds)
        {
            if (col.transform != transform && flag)
            {
                target = col.transform;

                break;
            }
            if (flag == false)
            {
                break;
            }
        }
    }

    protected virtual void UpdAnim()
    {
        an.SetBool("isDead",!isAlive);
        an.SetBool("isDamage", isHearting);
        //an.SetBool("isAttacking", isAttacking);
        an.SetBool("isIDLE", isIdle);
    }
    private void CheckLayerStats()
    {
        canSeeAnotherEnemy = Physics2D.Raycast(otherEnemyCheck.position, transform.right, attackDistance * currentDirection * 0.2f, anotherEnemys);

        canSeeTarget = Physics2D.Raycast(targetsCheck.position, transform.right, targetSeeDistance * currentDirection, targLayer) && isAlive || Physics2D.Raycast(targetsCheck.position, transform.right, -plSeeDistanceBack * currentDirection, targLayer) && isAlive;
        canHurtTarget = Physics2D.Raycast(targetsCheck.position,transform.right, (attackDistance + attackDistanceError) * currentDirection, targLayer);
        canHurtObj = Physics2D.Raycast(targetsCheck.position, transform.right, attackDistance * currentDirection, destrObjLayer);

        Invoke("CheckJumpLayer", 1f);
    }

    private void CheckJumpLayer()
    {
        isWall = Physics2D.Raycast(wallCheck.position, transform.right, wallDistance * currentDirection, whichWall);
        isGround = Physics2D.OverlapCircle(groundCheck.position, grRad, whichWall);

    }
    #endregion

    #region Movement
    //патрулирование
    protected virtual void Patrol()
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
        if (!isAttacking) Move(chaseSpeed);
        else Move(attackSpeed);
        ChaseFlip();
    }
    protected void ChaseFlip()
    {
        var enemyX = transform.position.x;
        var targetX = target.position.x;
        if ((enemyX < targetX - 1f && currentDirection == -1) || (enemyX > targetX + 1f && currentDirection == 1))
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

    protected void Move(float speed)
    {
        if (canRun) rb.velocity = new Vector2(speed * currentDirection, rb.velocity.y);
        //else rb.velocity = new Vector2(0, rb.velocity.y);
    }
    #endregion

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

    #region Attack System
    private void CheckToAttack()
    {
        if (canHurtTarget && canAttack && !isAttacking || canHurtObj && canAttack && !isAttacking)
        {
            StartAttack();
        }
    }
    protected virtual void StartAttack()
    {
        isAttacking = true;
        canFlip = false;
        kickAud.Play();
        an.SetBool("isAttacking", isAttacking);
    }
    private void CheckAttackHitBox()
    {
        if (isAlive)
        {
            Collider2D[] detectedObjs = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attackRad, destrObjLayer + targLayer + otherObjLayer);
            foreach (Collider2D col in detectedObjs)
            {
                col.transform.SendMessage("Damage", damage * currentDirection);
            }

            if (HitBoxAud != null) HitBoxAud.Play();
        }
    }
    protected virtual void EndAttack()
    {
        isAttacking = false;
        canFlip = true;
        an.SetBool("isAttacking", false);

        attackDistanceError = Random.Range(0, attackDistanceErrorMax);
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

        KnockEffect(damage);
        BloodEffect();
        DamageSoundEffect();

        StartFight();
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

        var x = Instantiate(money, transform.position, Quaternion.identity, null);
        x.GetComponent<Rigidbody2D>().velocity = new Vector2(knockSpeedX / 3, knockSpeedY / 3);

        Destroy(hitZone);

        CheckDeadHitBox();

        Destroy(this);

        sceneData.SetObjDisabled(isRespawn, idInScene);
    }

    protected virtual void EndPain()
    {
        isHearting = false;
    }

    protected void CheckDeadHitBox()
    {
        Collider2D[] detectedObjs = Physics2D.OverlapCircleAll(transform.position, 15f, targLayer);
        foreach (Collider2D col in detectedObjs)
        {
            col.transform.SendMessage("StopFight");
        }
    }

    #endregion

    #region Effects
    private void BloodEffect()
    {
        particle.Play();
        Instantiate(bloodPrefs[Random.Range(0, bloodPrefs.Length)], targetsCheck.position, Quaternion.identity, null);
    }

    private void KnockEffect(float damage)
    {
        if (damage > 0) knockSpeedX = Mathf.Abs(knockSpeedX);
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
        KnockEffect(currentDirection);
    }
    #endregion

    #region For Set Status of Behavior (PRIVATE)

    protected virtual void StopFight()
    {
        target = null;
        
        StopEnemy();
        DizableCombat();
        UnFreezeEnemy();
        EnableCombat();
        seeTarget = false;
        //isIdle = true;
        rb.velocity = Vector2.zero;
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
        EndAttack();
        canAttack = false;
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
        return seeTarget;
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


    protected void ChangeHealth(float hp)
    {
        health = hp;
    }
    #endregion

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attackRad);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(targetsCheck.position, new Vector3(targetsCheck.position.x + targetSeeDistance * currentDirection, targetsCheck.position.y, targetsCheck.position.z) );
        Gizmos.DrawLine(targetsCheck.position, new Vector3(targetsCheck.position.x + - targetSeeDistance * currentDirection / 2, targetsCheck.position.y, targetsCheck.position.z));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(targetsCheck.position, new Vector3(targetsCheck.position.x + attackDistance * currentDirection, targetsCheck.position.y, targetsCheck.position.z));        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x - patroolDistance,transform.position.y), 1f);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + patroolDistance, transform.position.y), 1f);

        Gizmos.DrawLine(otherEnemyCheck.position, new Vector3(otherEnemyCheck.position.x + attackDistance *0.6f * currentDirection, otherEnemyCheck.position.y, otherEnemyCheck.position.z));
        Gizmos.color = Color.green;

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallDistance * currentDirection, wallCheck.position.y, wallCheck.position.z));
        Gizmos.DrawWireSphere(groundCheck.position, grRad);

    }
}
